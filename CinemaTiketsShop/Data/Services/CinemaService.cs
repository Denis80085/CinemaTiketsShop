using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Data.Base.CacheDecoration;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.Services.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

namespace CinemaTiketsShop.Data.Services
{
    public class CinemaService : EntityBaseRepo<Cinema>, ICinemaService
    {
        private readonly ApplicationDbConntext _context;

        public CinemaService(IRedisCachingService cach, ApplicationDbConntext conntext) : base(conntext, cach, "Cinema")
        {
            _context = conntext;
        }

        public async Task<Cinema?> IncludeMovies(Cinema cinema)
        {
            var FoundCinema = await _context.Cinemas.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == cinema.Id);

            return FoundCinema;
        } 
    }
}
