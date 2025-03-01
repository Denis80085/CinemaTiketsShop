using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.MovieVMs;

namespace CinemaTiketsShop.Mappers.MovieMappers
{
    public static class MovieMapper
    {
        public static IndexMovieViewModel MapIndexVM(this Movie MovieModel)
        {
            return new IndexMovieViewModel
            {
                Id = MovieModel.Id,
                Name = MovieModel.Name,
                Description = MovieModel.Description,
                StartDate = MovieModel.StartDate,
                EndDate = MovieModel.EndDate,
                Price = MovieModel.Price,
                Category = MovieModel.Category,
                Logo = MovieModel.Logo,
                CinemaId = MovieModel.CinemaId,
                CinemaName = MovieModel.Cinema!.Name,
                ProducerId = MovieModel.ProducerId,
                ProducerName = MovieModel.Producer!.Name,
                PublicId = MovieModel.PublicId
            };
        }

        public static Movie MapMovieModel(this CreateMovieViewModel MovieVM, UploadedImageResult res)
        {
            return new Movie
            {
                Name = MovieVM.Name,
                Description = MovieVM.Bio!,
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
