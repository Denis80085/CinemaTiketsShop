using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.IdentityServerData.Dtos
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [AllowNull]
        public string? Titel { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
