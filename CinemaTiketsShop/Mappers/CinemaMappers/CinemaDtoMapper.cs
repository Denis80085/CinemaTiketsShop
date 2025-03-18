using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.BaseAbstractVMs;
using CinemaTiketsShop.ViewModels.CinemaVMs;

namespace CinemaTiketsShop.Mappers.CinemaMappers
{
    public static class IndexCinemaMapper
    {
        public static CinemaDto MapIndexCinema(this Cinema CinemaModel)
        {
            return new CinemaDto
            {
                Id = CinemaModel.Id,
                Name = CinemaModel.Name,
                LogoUrl = CinemaModel.LogoUrl,
                Description = CinemaModel.Description,
                MoviesQueue = new Queue<ItemSelect?>(CinemaModel.Movies!.Where(m => m is not null).Select(m => new ItemSelect
                {
                    Id = m.Id,
                    Name = m.Name,
                    Picture = m.Logo
                }))
            };
        }
    }
}
