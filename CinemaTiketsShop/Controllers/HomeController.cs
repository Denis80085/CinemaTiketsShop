using System.Diagnostics;
using CinemaTiketsShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTiketsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            
        }

    }
}
