using CinemaTiketsShop.Models;
using CinemaTiketsShop.QueryObjects.MoviesQuery;

namespace CinemaTiketsShop.Data.Services
{
    public interface IMovieService
    {
        Task<Movie?> Create(Movie entity, List<int> ActorsId);
        Task<IEnumerable<Movie>> GetAll(MovieQueryObj movieQuery);
        Task<Movie?> GetById(int id);
    }
}
