using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public interface IMovieService
    {
        Task<Movie?> Create(Movie entity, List<int> ActorsId);
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie?> GetById(int id);
    }
}
