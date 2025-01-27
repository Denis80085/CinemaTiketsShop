using CinemaTiketsShop.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAll();
            return View(movies);
        }
    }
}
