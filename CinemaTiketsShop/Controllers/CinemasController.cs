using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.CinemaVMs;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTiketsShop.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly ILogger<CinemasController> _logger;
        private readonly IPictureUploader _pictureUploader;

        public CinemasController( ICinemaService cinemaService, ILogger<CinemasController> logger, IPictureUploader pictureUploader)
        {
            _cinemaService = cinemaService;
            _logger = logger;
            _pictureUploader = pictureUploader;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var Cinemas = await _cinemaService.GetAll();
                return View(Cinemas);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return View("Empty");
            }
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Bio, Foto, PictureUrl")] CreateCinemaViewModel CinemaVM, [FromForm] string Picture_Upload_Method)
        {
            if (!ModelState.IsValid) 
            {
                return View(CinemaVM);
            }

            if (CinemaVM.Foto == null && string.IsNullOrWhiteSpace(CinemaVM.PictureUrl))
            {
                ModelState.AddModelError("Foto", "No picture was found");

                return View(CinemaVM);
            }

            try 
            {
                var UploadImageRes = new UploadedImageResult();

                if(Picture_Upload_Method == "FromDevice") 
                {
                    UploadImageRes = await _pictureUploader.UploadNewImageFromFileAsync(CinemaVM.Foto);
                }
                else 
                {
                    UploadImageRes = await _pictureUploader.UploadNewImageFromUrl(CinemaVM.PictureUrl);
                }

                if (UploadImageRes.ErrorAcured) 
                {
                    ModelState.AddModelError(UploadImageRes.ErrorAt, UploadImageRes.ErrorMessage);
                    return View(CinemaVM);
                }

                var NewCinema = new Cinema
                {
                    Name = CinemaVM.Name,
                    Description = CinemaVM.Bio,
                    LogoUrl = UploadImageRes.PictureUrl,
                    PublicId = UploadImageRes.PublicId
                };

                var CreatedCinema = await _cinemaService.Create(NewCinema);

                if (CreatedCinema != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else 
                    return View("Empty;");
            }
            catch(OperationCanceledException ex) 
            {
                _logger.LogCritical(ex, "Cinema could not be created with message: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return View("Empty");
            }
        }
    }
}
