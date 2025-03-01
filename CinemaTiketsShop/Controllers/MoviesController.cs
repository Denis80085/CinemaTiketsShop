using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Dictionarys;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Mappers.MovieMappers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.QueryObjects.MoviesQuery;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.Services.Redis;
using CinemaTiketsShop.ViewModels.BaseAbstractVMs;
using CinemaTiketsShop.ViewModels.MovieVMs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Collections;

namespace CinemaTiketsShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IActorServices _actorsService;
        private readonly ICinemaService _cinemaService;
        private readonly IProducerService _producerService;
        private readonly ModelDictionary modelDictionarySelector;
        private readonly IPictureUploader _pictureUploader;
        private readonly ILogger<MoviesController> _logger;
        private readonly IPhotoService _photoService;
        private readonly IRedisCachingService _CacheService;

        public MoviesController(IMovieService movieService, IActorServices actorService, ICinemaService cinemaService, 
            IProducerService  producerService, IPictureUploader pictureUploader, ILogger<MoviesController> logger, IPhotoService photoService, IRedisCachingService CacheService)
        {
            _movieService = movieService;
            _actorsService = actorService;
            _producerService = producerService;
            _cinemaService = cinemaService;
            modelDictionarySelector = new ModelDictionary(actorService, producerService, cinemaService);
            _pictureUploader = pictureUploader;
            _logger = logger;
            _photoService = photoService;
            _CacheService = CacheService;
        }

        public async Task<IActionResult> Index([FromQuery]MovieQueryObj movieQuery)
        {
            var Movies = await _CacheService.GetValues<IndexMovieViewModel>($"Movie-OfCinema-{movieQuery.OfCinemaId}");

            if (Movies.Any())
            {
                return View(Movies);
            }

            var MoviesModel = await _movieService.GetAll(movieQuery);

            if(MoviesModel is null) 
            {  
                return View(null); 
            }

            IEnumerable<IndexMovieViewModel>? MovieVMs = MoviesModel.Where(m => m.Cinema is not null).Select(m => m.MapIndexVM());

            if(MovieVMs is not null) 
            {
                await _CacheService.SetValues($"Movie-OfCinema-{MovieVMs.First().CinemaId}", MovieVMs, 5);
            }
            
            return View(MovieVMs);
        }

        //Seeds Actors, Producers tables and Cinemas select in create view
        private async Task InjectCreateTables() 
        {
            IDictionary<int, ItemSelect> ActorDic = await modelDictionarySelector.SelectActorsKeysAndNames();
            IDictionary<int, ItemSelect> ProducerDic = await modelDictionarySelector.SelectProducersKeysAndNames();
            IDictionary<int, string> CinemaDic = await modelDictionarySelector.SelectCinemasKeysAndNames();

            ViewData["Actors"] = ActorDic;
            ViewData["Producers"] = ProducerDic;
            ViewData["Cinemas"] = CinemaDic;
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            await InjectCreateTables();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieViewModel MovieVM, string Picture_Upload_Method)
        {
            if (!ModelState.IsValid) 
            {
                await InjectCreateTables();

                return View(MovieVM);
            }

            if(string.IsNullOrEmpty(MovieVM.PictureUrl) && MovieVM.Foto is null) 
            {
                await InjectCreateTables();
                ModelState.AddModelError("Foto", "Please add a picture for the movie");
                return View(MovieVM);
            }

            if (!MovieVM.SelActors.Any()) 
            {
                await InjectCreateTables();
                ModelState.AddModelError("SelActors", "Please select at least one actor");
                return View(MovieVM);
            }
            
            var UploadRes = new UploadedImageResult();
            try
            {
                if (Picture_Upload_Method == "FromDevice") 
                {
                    UploadRes = await _pictureUploader.UploadNewImageFromFileAsync(MovieVM.Foto);
                }

                if(Picture_Upload_Method  == "FromUrl") 
                {
                    UploadRes = await _pictureUploader.UploadNewImageFromUrl(MovieVM.PictureUrl);
                }

                if (UploadRes.ErrorAcured) 
                {
                    ModelState.AddModelError(UploadRes.ErrorAt, UploadRes.ErrorMessage);
                }

                if (!UploadRes.Succeded) 
                {
                    ModelState.AddModelError("Foto", "Picture upload failed");
                }

                if(ModelState.ErrorCount > 0) 
                {
                    await _photoService.DeletePhotoAsync(UploadRes.PublicId);
                    await InjectCreateTables();
                    return View(MovieVM);
                }

                var newMovie = await _movieService.Create(MovieVM.MapMovieModel(UploadRes), MovieVM.SelActors);

                if (newMovie != null)
                {
                    await _CacheService.SetVal("Movie", newMovie, 5);

                    return RedirectToAction("Index");
                }
                else 
                {
                    await _photoService.DeletePhotoAsync(UploadRes.PublicId);
                    return View("Empty");
                }
            }
            catch (Exception ex) 
            {
                await _photoService.DeletePhotoAsync(UploadRes.PublicId);

                _logger.LogCritical($"Movie Creation failed: {ex.Message}");
                return View("Empty");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute]int id) 
        {
            try
            {
                var Movie = await _movieService.GetById(id);

                if(Movie is null) 
                {
                    return View("Empty");
                }

                return View(Movie);
            }
            catch (Exception ex) 
            {
                _logger.LogCritical($"Movie details failed: {ex.Message}");
                return View("Empty");
            }
        }
    }
}
