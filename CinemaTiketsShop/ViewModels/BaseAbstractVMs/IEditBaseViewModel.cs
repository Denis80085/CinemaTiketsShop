using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.ViewModels.BaseAbstractVMs
{
    public interface IEditBaseViewModel
    {
        int Id { get; set; }

        string Name { get; set; }

        string? Bio { get; set; }

        string? PublicId { get; set; }

        IFormFile? Foto { get; set; }

        string PictureUrl { get; set; }

        string OldPictureUrl { get; set; }
    }
}
