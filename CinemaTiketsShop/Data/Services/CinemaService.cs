using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CinemaTiketsShop.Data.Services
{
    public class CinemaService : EntityBaseRepo<Cinema>, ICinemaService
    {
        private readonly ApplicationDbConntext _context;

        public CinemaService(ApplicationDbConntext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cinema?> IncludeMovies(Cinema cinema)
        {
            var FoundCinema = await _context.Cinemas.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == cinema.Id);

            return FoundCinema;
        } 
    }
}
