using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CinemaTiketsShop.ViewModels.BaseAbstractVMs
{
    public class CreateBaseViewModel : ICreateBaseViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(270, MinimumLength = 2, ErrorMessage = "Full name must contain at least 2 and maximum 70 chars")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Biography")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Biography must contain at least 2 and maximum 500 chars")]
        virtual public string? Bio { get; set; }

        public IFormFile? Foto { get; set; }

        [Url]
        public string? PictureUrl { get; set; }
    }
}
