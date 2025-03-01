using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Mappers.CinemaMappers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.Services.Redis;
using CinemaTiketsShop.ViewModels.CinemaVMs;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Data.Services
{
    public class CinemaService : EntityBaseRepo<Cinema>, ICinemaService
    {
        private readonly ApplicationDbConntext _context;
        private readonly IRedisCachingService _cache;

        public CinemaService(IRedisCachingService cach, ApplicationDbConntext conntext) : base(conntext, cach, "Cinema")
        {
            _context = conntext;
            _cache = cach;
        }

        public async Task<IEnumerable<CinemaDto>> GetCinemaDtos()
        {
            var cinemasDto = await _cache.GetValues<CinemaDto>(cache_key);

            if (cinemasDto.Any())
            {
                return cinemasDto;
            }

            var cinemas = await _context.Cinemas.Include(c => c.Movies).Select(c => c.MapIndexCinema()).ToListAsync();

            await _cache.SetValues(cache_key, cinemas, TTL_Min);

            return cinemas;
        }
        public async Task<Cinema?> IncludeMovies(Cinema cinema)
        {
            var FoundCinema = await _context.Cinemas.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == cinema.Id);

            return FoundCinema;
        }
    }
}
