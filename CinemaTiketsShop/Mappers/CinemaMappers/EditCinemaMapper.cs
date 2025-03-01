using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.BaseAbstractVMs;
using CinemaTiketsShop.ViewModels.CinemaVMs;

namespace CinemaTiketsShop.Mappers.CinemaMappers
{
    public static class EditCinemaMapper
    {
        public static EditCinemaViewModel MapEditViewModel(this Cinema Model)
        {
            return new EditCinemaViewModel
            {
                Id = Model.Id,
                Name = Model.Name,
                Bio = Model.Description,
                PublicId = Model.PublicId,
                OldPictureUrl = Model.LogoUrl,
                PictureUrl = Model.LogoUrl
            };
        }

        public static Cinema MapCinemaModel(this EditBaseViewModel ViewModel)
        {
            return new Cinema
            {
                Id = ViewModel.Id,
                PublicId = ViewModel.PublicId,
                Name = ViewModel.Name,
                Description = ViewModel.Bio,
                LogoUrl = ViewModel.OldPictureUrl
            };
        }
    }
}
