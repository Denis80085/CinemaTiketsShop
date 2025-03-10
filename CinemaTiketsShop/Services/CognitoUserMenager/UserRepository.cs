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

        public UserRepository(IOptions<CognitoAppConfig> options)
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
        }

        public async Task<UserSignUpResponse> ConfirmUserSignUpAsync(UserConfirmSignUpModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSignUpResponse> CreateUserAsync(UserSignUpModel model)
        {
            throw new NotImplementedException();
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

                authResponseModel.IsSuccess = true;
                return authResponseModel;
            }
            //catch (UserNotConfirmedException)
            //{
            //    // Occurs if the User has signed up 
            //    // but has not confirmed his EmailAddress
            //    // In this block we try sending 
            //    // the Confirmation Code again and ask user to confirm
            //}
            catch (UserNotFoundException)
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
