using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.MovieVMs;

namespace CinemaTiketsShop.Mappers.MovieMappers
{
    public static class MovieMapper
    {
        public static Movie MapMovieModel(this CreateMovieViewModel MovieVM, UploadedImageResult res) 
        {
            return new Movie
            {
                Name = MovieVM.Name,
                Description = MovieVM.Bio,
                Logo = res.PictureUrl,
                PublicId = res.PublicId,
                Category = MovieVM.Category,
                Price = MovieVM.Price,
                StartDate = MovieVM.StartDate,
                EndDate = MovieVM.EndDate,
                CinemaId = MovieVM.CinemaId,
                ProducerId = MovieVM?.ProducerId
            };
        }
    }
}
