namespace CinemaTiketsShop.Models.UserModels
{
    public class UserConfirmSignUpModel : BaseUserModel
    {
        public string ConfirmCode { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
