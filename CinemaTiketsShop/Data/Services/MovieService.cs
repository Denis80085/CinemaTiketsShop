using CinemaTiketsShop.Models;
using CinemaTiketsShop.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace CinemaTiketsShop.Data.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbConntext _context;
        private readonly IActor_MovieService _ActorMovieService;

        public MovieService(ApplicationDbConntext context, IActor_MovieService ActorMovieService)
        {
            _context = context;
            _ActorMovieService = ActorMovieService;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.Include(m => m.Cinema).ToListAsync();
        }

        public async Task<Movie?> Create(Movie entity, List<int> ActorsId)
        {
            try
            {
                var EntRes = await _context.Movies.AddAsync(entity);

                if (EntRes.State is not EntityState.Added)
                {
                    await _context.DisposeAsync();
                    return null;
                }

                await _context.SaveChangesAsync();

                await _ActorMovieService.AddActorsToMovie(entity.Id, ActorsId);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Movie create faieled", ex);
            }
            
        }

    }
}
