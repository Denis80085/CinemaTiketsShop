using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.CinemaVMs;

namespace CinemaTiketsShop.Data.Services
{
    public interface ICinemaService : IEntityBaseRepo<Cinema>
    {
        Task<Cinema?> IncludeMovies(Cinema cinema);
        Task<IEnumerable<CinemaDto>> GetCinemaDtos();
    }
}
