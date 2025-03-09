using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.IdentityServerData.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First Name must be at least 2 and maximum 30 characters long")]
        public required string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last Name must be at least 2 and maximum 30 characters long")]
        public required string LastName { get; set; }
        [AllowNull]
        [StringLength(15, ErrorMessage = "Titel cant have more than 15 characters")]
        public string? Titel { get; set; }
    }
}
