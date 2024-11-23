using CinemaTiketsShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbConntext _context;

        public ActorsController(ApplicationDbConntext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Data = await _context.Actors.Select(a => a).ToListAsync();
            return View(Data);
        }
    }
}
