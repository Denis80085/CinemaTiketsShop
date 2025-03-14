namespace CinemaTiketsShop.Models.UserModels
{
    public class UserConfirmSignUpModel
    {
        public string ConfirmCode { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
