using CinemaTiketsShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Controllers
{
    public class ProducersController : Controller
    {
        private readonly ApplicationDbConntext _context;

        public ProducersController(ApplicationDbConntext context)
        {
            _context = context;
        }
        public  async Task<IActionResult> Index()
        {
            var Producers = await _context.Producers.Select(p => p).ToListAsync();

            return View(Producers);
        }
    }
}
