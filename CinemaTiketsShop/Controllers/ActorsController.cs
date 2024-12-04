using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Models;
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

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, FotoURL, Bio")]Actor actor) 
        {
            if (!ModelState.IsValid) 
            {
                return View(actor);
            }

            await _actorService.Create(actor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id) 
        {
            var Actor = await _actorService.GetById(id);

            if (Actor._isFound) 
            {
                return View(Actor._actor);
            }
            else 
            {
                return View("Empty");
            }
        }
    }
}
