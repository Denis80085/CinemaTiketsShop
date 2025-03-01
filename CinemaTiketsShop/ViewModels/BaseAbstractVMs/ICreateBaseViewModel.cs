namespace CinemaTiketsShop.ViewModels.BaseAbstractVMs
{
    public interface ICreateBaseViewModel
    {
        string Name { get; set; }

        string? Bio { get; set; }

        IFormFile? Foto { get; set; }

        string? PictureUrl { get; set; }
    }
}
