using CinemaTiketsShop.Models;
using CinemaTiketsShop.QueryObjects.MoviesQuery;
using CinemaTiketsShop.QuerySelectors.MovieSelector;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<IEnumerable<Movie>> GetAll(MovieQueryObj movieQuery)
        {

            var Query = _context.Movies.Include(m => m.Cinema).Include(m => m.Producer).AsQueryable().SelectByCinema(movieQuery.OfCinemaId);

            return await Query.ToListAsync();
        }

        public async Task<Movie?> GetById(int id)
        {
            try
            {
                var FoundMovie = _context.Movies.Where(m => m.Id == id).Include(m => m.Producer).Include(m => m.Cinema).Include(m => m.Movies_Actors!).ThenInclude(x => x.Actor).Select(m => m);


                return await FoundMovie.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Movie GetById", ex);
            }

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
