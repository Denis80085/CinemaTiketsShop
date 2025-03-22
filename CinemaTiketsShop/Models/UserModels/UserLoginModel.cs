using CinemaTiketsShop.Models.MessageModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.Models.UserModels
{
    public class UserLoginModel : BaseUserModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required")]
        override public string Email { get; set; } = string.Empty;

        public MessageModel? Message { get; set; }
    }
}
