using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.Models
{
    public class Producer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Full Name")]
        [StringLength(270, MinimumLength = 2, ErrorMessage = "Full name must contain at least 2 and maximum 70 chars")]
        public required string Name { get; set; }

        [Display(Name = "Biography")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Biography must contain at least 2 and maximum 500 chars")]
        public string? Bio { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public required string FotoURL { get; set; }

        //For Cloudinary
        [AllowNull]
        public string? PublicId { get; set; } 

        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
