using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using CinemaTiketsShop.Configs;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Models.UserModels;
using CinemaTiketsShop.ResponseDtos.UserResponsDtos;
using CinemaTiketsShop.Services.CognitoUserMenager.ActionModels;
using CinemaTiketsShop.Services.CognitoUserMenager.Token;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace CinemaTiketsShop.Services.CognitoUserMenager
{
    public class UserRepository : IUserRepository
    {
        private readonly CognitoAppConfig _cognitoAppConfig;
        private readonly AmazonCognitoIdentityProviderClient _provider;
        private readonly CognitoUserPool _userPool;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IOptions<CognitoAppConfig> options, ILogger<UserRepository> logger)
        {
            _cognitoAppConfig = options.Value;

            _provider = new AmazonCognitoIdentityProviderClient(
                _cognitoAppConfig.AccessKeyId,
                _cognitoAppConfig.AccessSecretKey,
                RegionEndpoint.GetBySystemName(_cognitoAppConfig.Region));

            _userPool = new CognitoUserPool(
                _cognitoAppConfig.UserPoolId,
                _cognitoAppConfig.AppClientId,
                _provider,
                _cognitoAppConfig.AppClientSecret);

            _logger = logger;
        }

        public async Task<UserSignUpResponse> ConfirmUserSignUpAsync(UserConfirmSignUpModel model)
        {
            string secret_hash = CognitoHasher.HashUsername(model.Email, _cognitoAppConfig.AppClientId, _cognitoAppConfig.AppClientSecret);

            ConfirmSignUpRequest request = new ConfirmSignUpRequest
            {
                ClientId = _cognitoAppConfig.AppClientId,
                ConfirmationCode = model.ConfirmCode,
                Username = model.Email,
                SecretHash = secret_hash
            };

            try
            {
                var response = await _provider.ConfirmSignUpAsync(request);

                return new UserSignUpResponse
                {
                    Email = model.Email,
                    UserId = model.UserId,
                    Message = "User Confirmed",
                    IsSuccess = true
                };
            }
            catch (CodeMismatchException)
            {
                return new UserSignUpResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Confirmation Code",
                    Email = model.Email
                };
            }
            catch (ExpiredCodeException) 
            {
                return new UserSignUpResponse
                {
                    IsSuccess = false,
                    Message = "Expired code. Please try again",
                    Email = model.Email
                };
            }
        }

        public async Task<UserSignUpResponse> CreateUserAsync(UserSignUpModel model)
        {
            string Secret_hash = CognitoHasher.HashUsername(model.UserName, _cognitoAppConfig.AppClientId, _cognitoAppConfig.AppClientSecret);
            // create a SignUpRequest
            var signUpRequest = new SignUpRequest
            {
                ClientId = _cognitoAppConfig.AppClientId,
                Password = model.Password,
                Username = model.UserName,
                SecretHash = Secret_hash,
                ClientMetadata = new Dictionary<string, string>
                {
                    { "email", model.Email }
                }
            };

            // add all the attributes 
            // you want to add to the New User
            signUpRequest.UserAttributes.Add(new AttributeType
            {
                Name = "email",
                Value = model.Email
            });
            signUpRequest.UserAttributes.Add(new AttributeType
            {
                Value = model.GivenName,
                Name = "given_name"
            });
            signUpRequest.UserAttributes.Add(new AttributeType
            {
                Value = model.FamilyName,
                Name = "family_name"
            });

            //if (model.ProfilePhoto != null)
            //{
            //    // upload the incoming profile photo to user's S3 folder
            //    // and get the s3 url
            //    // add the s3 url to the profile_photo attribute of the userCognito
            //    var picUrl = await _storage.AddItem(model.ProfilePhoto, "profile");

            //    signUpRequest.UserAttributes.Add(new AttributeType
            //    {
            //          Value = picUrl,
            //          Name = "picture"
            //    });
            //}

            try
            {
                // call SignUpAsync() method
                SignUpResponse response = await _provider.SignUpAsync(signUpRequest);

                var signUpResponse = new UserSignUpResponse
                {
                    UserId = response.UserSub,
                    Email = model.Email,
                    Message = $"Confirmation Code sent to   {response.CodeDeliveryDetails.Destination} via {response.CodeDeliveryDetails.DeliveryMedium.Value}",
                    IsSuccess = true
                };
                return signUpResponse;
            }
            catch (UsernameExistsException)
            {
                return new UserSignUpResponse
                {
                    IsSuccess = false,
                    Message = "Emailaddress Already Exists"
                };
            }
            catch (Exception ex)
            {
                return new UserSignUpResponse
                {
                    IsSuccess = false,
                    Message = ex.ToString()
                };
            }
        }

        public async Task<UserProfileResponse> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseModel> TryChangePasswordAsync(ChangePwdModel model)
        {
            throw new NotImplementedException();
        }

        public Task<InitForgotPwdResponse> TryInitForgotPasswordAsync(InitForgotPwdModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponseModel> TryLoginAsync(UserLoginModel model)
        {
            try
            {
                CognitoUser user = new CognitoUser(
                        model.Email,
                        _cognitoAppConfig.AppClientId,
                        _userPool,
                        _provider,
                        _cognitoAppConfig.AppClientSecret);

                string Secret_hash = CognitoHasher.HashUsername(model.Email, _cognitoAppConfig.AppClientId, _cognitoAppConfig.AppClientSecret);

                InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
                {
                    Password = model.Password,
                    ClientMetadata = new Dictionary<string, string>
                    {
                        { "SECRET_HASH",  Secret_hash}
                    }
                };

                AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest);
                var result = authResponse.AuthenticationResult;

                var authResponseModel = new AuthResponseModel();
                authResponseModel.Email = user.UserID;
                authResponseModel.UserId = user.Username;
                authResponseModel.Tokens = new TokenModel
                {
                    IdToken = result.IdToken,
                    AccessToken = result.AccessToken,
                    ExpiresIn = result.ExpiresIn,
                    RefreshToken = result.RefreshToken
                };

                var userResponse = await user.GetUserDetailsAsync();

                var email_verified = userResponse.UserAttributes.Find(u => u.Name == "email_verified");

                if (email_verified != null && email_verified.Value != "true")
                {
                    authResponseModel.IsUserConfirmed = false;
                }
                else 
                { 
                    authResponseModel.IsUserConfirmed = true; 
                }

                authResponseModel.IsSuccess = true;
                return authResponseModel;
            }
            catch (UserNotConfirmedException)
            {
                return new AuthResponseModel
                {
                    IsSuccess = false,
                    Message = "User not confirmed"
                };
            }
            catch (UserNotFoundException ex)
            {
                // Occurs if the provided emailAddress 
                // doesn't exist in the UserPool
                return new AuthResponseModel
                {
                    IsSuccess = false,
                    Message = "EmailAddress not found."
                };
            }
            catch (NotAuthorizedException ex)
            {
                return new AuthResponseModel
                {
                    IsSuccess = false,
                    Message = "Incorrect username or password"
                };
            }
            catch(Exception ex) 
            {
                return new AuthResponseModel
                {
                    IsSuccess = false,
                    Message = ex.ToString()
                };
            }
        }

        public async Task<UserSignOutResponse> TryLogOutAsync(UserSignOutModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResetPasswordResponse> TryResetPasswordWithConfirmationCodeAsync(ResetPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateProfileResponse> UpdateUserAttributesAsync(UpdateProfileModel model)
        {
            throw new NotImplementedException();
        }
    }
}
