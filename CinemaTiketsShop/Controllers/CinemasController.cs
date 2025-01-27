using CinemaTiketsShop.Data.Services;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTiketsShop.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;

        public CinemasController( ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public async Task<IActionResult> Index()
        {
            var Cinemas = await _cinemaService.GetAll();
            return View(Cinemas);
        }
    }
}
