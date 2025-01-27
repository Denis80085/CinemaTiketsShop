using CinemaTiketsShop.Models;
using CinemaTiketsShop.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Data.Services
{
    public class MovieService : EntityBaseRepo<Movie>, IMovieService
    {
        private readonly ApplicationDbConntext _context;

        public MovieService(ApplicationDbConntext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.Include(m => m.Cinema).ToListAsync();
        }
    }
}
