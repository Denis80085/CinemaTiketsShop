using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CinemaTiketsShop.ViewModels.ActorVMs;
using CinemaTiketsShop.Helpers;
using CloudinaryDotNet.Actions;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.Mappers.ActorMappers;

namespace CinemaTiketsShop.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorServices _actorService;
        private readonly ILogger _logger;
        private readonly IPhotoService _photoService;
        private readonly IPictureUploader _pictureUploader;

        public ActorsController(IActorServices actorService, ILogger<ActorsController> logger, IPhotoService photoService, IPictureUploader pictureUploader)
        {
            _actorService = actorService;
            _logger = logger;
            _photoService = photoService;
            _pictureUploader = pictureUploader;
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
        public async Task<IActionResult> Create([Bind("Name, PictureUrl, Bio, Foto")]CreateActorViewModel ActorVM, [FromForm] string Picture_Upload_Method) 
        {
            if (!ModelState.IsValid) 
            {
                Debug.WriteLine("Actor not created", category: "Actor Validation Error by creating:");
                return View(ActorVM);
            }

            if (ActorVM.Foto == null && string.IsNullOrWhiteSpace(ActorVM.PictureUrl))
            {
                ModelState.AddModelError("Foto", "No picture was found");

                return View(ActorVM);
            }

            try 
            {
                var result = new ImageUploadResult();

                if (Picture_Upload_Method == "FromDevice" && ActorVM.Foto != null)
                {
                    result = await _photoService.UploadPhotoAsync(ActorVM.Foto);
                }

                if (Picture_Upload_Method == "FromUrl" && !string.IsNullOrWhiteSpace(ActorVM.PictureUrl))
                {
                    if (!await PictureUrl.isValid(ActorVM.PictureUrl))
                    {
                        ModelState.AddModelError("PictureUrl", "Url validation failed. Make sure that it is pointed to a image of type .jpg, .png, .webp or .svg");
                        return View(ActorVM);
                    }

                    result = await _photoService.UploadPhotoWithUrlAsync(ActorVM.PictureUrl);
                }

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ModelState.AddModelError("PictureUrl", $"Error by uploading the image. Error {result.StatusCode}");
                    return View(ActorVM);
                }

                var actor = new Actor
                {
                    Name = ActorVM.Name,
                    Bio  = ActorVM.Bio,
                    FotoURL = result.Url.ToString(),
                    PublicId = result.PublicId
                };

                await _actorService.Create(actor);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                _logger.LogCritical(ex, "Actor Controler Error at Create");

                return View("Empty");
            }

        }

        [HttpGet("Actors/Details/{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id) 
        {
            var Actor = await _actorService.GetActorResultById(id);

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
            var ActorResult = await _actorService.GetActorResultById(id);

            if (ActorResult._isFound && ActorResult._actor != null) 
            {
                return View(ActorResult._actor.MapEditActorViewModel());
            }
            else 
            {
                Debug.WriteLine("Actor Not Found", category: "Edit Controler Error:");
                return View("Empty");
            }

        }

        [HttpPost("Edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]string picture_change_method, [Bind("Id, Name, FotoUrl, OldFotoUrl, Bio, PublicId, Foto")] EditActorViewModel ActorVM)
        {
            if (!ModelState.IsValid) 
            {
                return View(ActorVM);
            }

            UploadedImageResult result = new UploadedImageResult(false);

            if (picture_change_method == "FromDevice" && ActorVM.Foto != null)
            {
                result = await _pictureUploader.UpdateImageFromFileAsync(ActorVM.Foto, ActorVM.PublicId);
            }

            if (picture_change_method == "FromUrl" && ActorVM.FotoUrl != ActorVM.OldFotoUrl)
            {
                result = await _pictureUploader.UpdateImageFromUrlAsync(ActorVM.FotoUrl, ActorVM.PublicId);
            }

            if (result.ErrorAcured)
            {
                ModelState.AddModelError(result.ErrorAt, result.ErrorMessage);
                return View(ActorVM);
            }

            if (result.Succeded)
            {
                ActorVM.OldFotoUrl = result.PictureUrl;
                ActorVM.PublicId = result.PublicId;
            }

            var NewActor = await _actorService.Update(id, ActorVM.MapActorModel());

            if (NewActor != null)
            {
                return RedirectToAction(nameof(Index));
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
            var ActorResult = await _actorService.GetActorResultById(id);

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
        public async Task<IActionResult> Delete([Bind("Id, Name, FotoURL, Bio, PublicId")] Actor deleteActor)
        {
            if(await _actorService.Delete(deleteActor) != null) 
            {
                await _photoService.DeletePhotoAsync(deleteActor.PublicId);

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
