using CinemaTiketsShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbConntext _context;

        public MoviesController(ApplicationDbConntext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Select(m =>  m).Include(x => x.Cinema).OrderBy(m => m.Name).ToListAsync();
            return View(movies);
        }
    }
}
