using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models.UserModels
{
    public abstract class BaseUserModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required")]
        public virtual string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
