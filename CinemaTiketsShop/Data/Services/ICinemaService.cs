using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public interface ICinemaService : IEntityBaseRepo<Cinema>
    {
        Task<Cinema?> IncludeMovies(Cinema cinema);
    }
}
