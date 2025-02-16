using CinemaTiketsShop.Models;
using System.Data;

namespace CinemaTiketsShop.Data.Services
{
    public interface IActor_MovieService
    {
        Task AddActorsToMovie(int MovieId, List<int> ActorsId);
    }
}
