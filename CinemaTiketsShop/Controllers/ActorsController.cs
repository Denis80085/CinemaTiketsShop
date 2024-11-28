using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorServices _actorService;

        public ActorsController(IActorServices actorService)
        {
            _actorService = actorService;
        }

        public async Task<IActionResult> Index()
        {
            var Data = await _actorService.GetActors();
            return View(Data);
        }
    }
}
