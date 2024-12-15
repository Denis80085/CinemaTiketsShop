using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                Debug.WriteLine("Actor not created", category: "Actor Validation Error by creating:");
                return View(actor);
            }

            await _actorService.Create(actor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Actors/Details/{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id) 
        {
            var Actor = await _actorService.GetById(id);

            if (Actor._isFound) 
            {
                return View(Actor._actor);
            }
            else 
            {
                Debug.WriteLine("Actor Not Found", category: "Details Controler Error:");
                return View("Empty");
            }
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id) 
        {
            var ActorResult = await _actorService.GetById(id);

            if (ActorResult._isFound) 
            {
                return View(ActorResult._actor);
            }
            else 
            {
                Debug.WriteLine("Actor Not Found", category: "Edit Controler Error:");
                return View("Empty");
            }

        }

        [HttpPost("Edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [Bind("Id, Name, FotoURL, Bio")] Actor actor)
        {
            if (!ModelState.IsValid) 
            {
                return View(actor);
            }

            var NewActor = await _actorService.Update(id, actor);

            if (NewActor != null)
            {
                return RedirectToAction(nameof(Details), new { id = id });
            }
            else
            {
                Debug.WriteLine("Actor Not Found", category: "Edit Controler Error:");
                return View("Empty");
            }
        }

        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id) 
        {
            var ActorResult = await _actorService.GetById(id);

            if (ActorResult._isFound && ActorResult._actor != null)
            {
                return View(ActorResult._actor);
            }
            else
            {
                Debug.WriteLine("Actor Not Found", category: "Delete Controler Error:");
                return View("Empty");
            }
        }

        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete([Bind("Id, Name, FotoURL, Bio")] Actor deleteActor)
        {
            if(await _actorService.Delete(deleteActor) != null) 
            {
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                Debug.WriteLine("Actor Not Found", category: "HDelete Controler Error:");
                return RedirectToAction(nameof(Index));
            }        
        }
    }
}
