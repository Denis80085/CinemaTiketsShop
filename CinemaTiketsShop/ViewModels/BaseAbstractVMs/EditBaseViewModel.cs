
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.ViewModels.BaseAbstractVMs
{
    public class EditBaseViewModel : IEditBaseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(270, MinimumLength = 2, ErrorMessage = "Full name must contain at least 2 and maximum 70 chars")]
        public required string Name { get; set; }

        [Display(Name = "Biography")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Biography must contain at least 2 and maximum 500 chars")]
        virtual public string? Bio { get; set; }

        [AllowNull]
        public string? PublicId { get; set; }

        public IFormFile? Foto { get; set; }

        [Url]
        public string PictureUrl { get; set; } = string.Empty;

        [Url]
        public required string OldPictureUrl { get; set; }
    }
}
