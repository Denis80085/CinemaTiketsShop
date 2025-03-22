
using CinemaTiketsShop.Models.UserModels;
using CinemaTiketsShop.ResponseDtos.UserResponsDtos;
using CinemaTiketsShop.Services.CognitoUserMenager.ActionModels;

namespace CinemaTiketsShop.Services.CognitoUserMenager
{
    public interface IUserRepository
    {
        /* Signup Flow Starts */
        Task<UserSignUpResponse> ConfirmUserSignUpAsync(UserConfirmSignUpModel model);
        Task<UserSignUpResponse> CreateUserAsync(UserSignUpModel model);
        /* Signup Flow Ends */

        /* Change Password Flow */
        Task<BaseResponseModel> TryChangePasswordAsync(ChangePwdModel model);

        /* Forgot Password Flow Starts */
        Task<InitForgotPwdResponse> TryInitForgotPasswordAsync(InitForgotPwdModel model);
        Task<ResetPasswordResponse> TryResetPasswordWithConfirmationCodeAsync(ResetPasswordModel model);
        /* Forgot Password Flow Ends */

        /* Login Flow Starts */
        Task<AuthResponseModel> TryLoginAsync(UserLoginModel model);
        /* Login Flow Ends */

        Task<UserSignOutResponse> TryLogOutAsync(UserSignOutModel model);

        /* Update Profile Flow Starts */
        Task<UserProfileResponse> GetUserAsync(string userId);
        Task<UserProfileResponse> GetUserByEmailAsync(string Email);
        Task<UpdateProfileResponse> UpdateUserAttributesAsync(UpdateProfileModel model);
        /* Update Profile Flow Ends */
        Task<UserResendConfirmCodeResponse> ResendConfirmationCodeAsync(UserResendConfirmCodeModel model);

    }
}
