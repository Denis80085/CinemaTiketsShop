using CinemaTiketsShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ApplicationDbConntext _context;

        public CinemasController(ApplicationDbConntext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Cinemas = await _context.Cinemas.Select(c => c).ToListAsync();
            return View(Cinemas);
        }
    }
}
