using Microsoft.Build.Framework;

namespace CinemaTiketsShop.Models.UserModels
{
    public class UserProfileModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string GivenName { get; set; } = string.Empty;
        [Required]
        public string FamilyName { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public string UserStatus { get; set; } = string.Empty;
    }
}
