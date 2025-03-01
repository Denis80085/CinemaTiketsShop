using CinemaTiketsShop.Models;
using CinemaTiketsShop.QueryObjects.MoviesQuery;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CinemaTiketsShop.QuerySelectors.MovieSelector
{
    public static class MovieQuerySelector
    {
        public static IQueryable<Movie> SelectByCinema(this IQueryable<Movie> Query, int? OfCinemaId) 
        {
            if(OfCinemaId is null) 
            {
                var FirstCinemaId = Query.First().CinemaId;
                return Query.Where(m => m.CinemaId == FirstCinemaId);
            }
            return Query.Where(m => m.CinemaId == OfCinemaId);
        }
    }
}
