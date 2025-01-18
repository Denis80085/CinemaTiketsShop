using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.ViewModels.ProducerVMs
{
    public class EditProducerViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(270, MinimumLength = 2, ErrorMessage = "Full name must contain at least 2 and maximum 70 chars")]
        public required string Name { get; set; }

        [Display(Name = "Biography")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Biography must contain at least 2 and maximum 500 chars")]
        public string? Bio { get; set; }

        [AllowNull]
        public string? PublicId { get; set; }

        public IFormFile? Foto { get; set; }

        [Url]
        public required string PictureUrl { get; set; }

        [Url]
        public string? OldPictureUrl { get; set; }
    }
}
