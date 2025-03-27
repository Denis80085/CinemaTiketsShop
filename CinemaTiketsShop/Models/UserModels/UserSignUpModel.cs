using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models.UserModels
{
    public class UserSignUpModel : BaseUserModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        override public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "User Name is required")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public required string GivenName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public required string FamilyName { get; set; }
    }
}
