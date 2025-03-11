namespace CinemaTiketsShop.Models.UserModels
{
    public class UserSignUpModel : BaseUserModel
    {
        public required string UserName { get; set; }
        public required string GivenName { get; set; }
        public required string FamilyName { get; set; }
    }
}
