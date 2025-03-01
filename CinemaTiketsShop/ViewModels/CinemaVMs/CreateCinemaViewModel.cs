using CinemaTiketsShop.ViewModels.BaseAbstractVMs;
using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.ViewModels.CinemaVMs
{
    public class CreateCinemaViewModel : CreateBaseViewModel
    {
        [Display(Name = "Description")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Description must contain at least 2 and maximum 500 chars")]
        override public string? Bio { get; set; }
    }
}
