using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Dictionarys;
using CinemaTiketsShop.ViewModels.ActorVMs;
using Microsoft.AspNetCore.Mvc;
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

        public MoviesController(IMovieService movieService, IActorServices actorService, ICinemaService cinemaService, IProducerService  producerService)
        {
            _movieService = movieService;
            _actorsService = actorService;
            _producerService = producerService;
            _cinemaService = cinemaService;
            modelDictionarySelector = new ModelDictionary(actorService, producerService, cinemaService);
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAll();
            return View(movies);
        }

        public async Task<IActionResult> Create() 
        {
            IDictionary<int, ActorSelectorView> ActorDic = await modelDictionarySelector.SelectActorsKeysAndNames();
            IDictionary<int, string> ProducerDic = await modelDictionarySelector.SelectProducersKeysAndNames();
            IDictionary<int, string> CinemaDic = await modelDictionarySelector.SelectCinemasKeysAndNames();

            ViewData["Actors"] = ActorDic;
            ViewData["Producers"] = ProducerDic;
            ViewData["Cinemas"] = CinemaDic;

            return View();
        }
    }
}
