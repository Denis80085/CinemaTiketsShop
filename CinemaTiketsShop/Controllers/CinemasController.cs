using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Helpers.MessageProviderHellper;
using CinemaTiketsShop.Mappers.CinemaMappers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.Models.MessageModels;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.ViewModels.CinemaVMs;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTiketsShop.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly ILogger<CinemasController> _logger;
        private readonly IPictureUploader _pictureUploader;
        private readonly IPhotoService _photoService;

        public CinemasController(ICinemaService cinemaService, ILogger<CinemasController> logger, IPictureUploader pictureUploader, IPhotoService photoService)
        {
            _cinemaService = cinemaService;
            _logger = logger;
            _pictureUploader = pictureUploader;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var Cinemas = await _cinemaService.GetCinemaDtos();
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

                if (Picture_Upload_Method == "FromDevice")
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
            catch (OperationCanceledException ex)
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

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            try
            {
                var Cinema = await _cinemaService.GetById(id);

                return View(Cinema.MapEditViewModel());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogCritical(ex, "Cinema could not be found with message: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return View("Empty");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, Name, PictureUrl, OldPictureUrl, Bio, Foto, PublicId")] EditCinemaViewModel CinemaViewModel, [FromForm] string picture_change_method)
        {
            if (!ModelState.IsValid)
            {
                return View(CinemaViewModel);
            }

            UploadedImageResult result = new UploadedImageResult(false);

            if (picture_change_method == "FromDevice" && CinemaViewModel.Foto != null)
            {
                result = await _pictureUploader.UpdateImageFromFileAsync(CinemaViewModel.Foto, CinemaViewModel.PublicId);
            }

            if (picture_change_method == "FromUrl" && CinemaViewModel.PictureUrl != CinemaViewModel.OldPictureUrl)
            {
                result = await _pictureUploader.UpdateImageFromUrlAsync(CinemaViewModel.PictureUrl, CinemaViewModel.PublicId);
            }

            if (result.ErrorAcured)
            {
                ModelState.AddModelError(result.ErrorAt, result.ErrorMessage);
                return View(CinemaViewModel);
            }

            if (result.Succeded)
            {
                CinemaViewModel.OldPictureUrl = result.PictureUrl;
                CinemaViewModel.PublicId = result.PublicId;
            }

            var Cinema = CinemaViewModel.MapCinemaModel();

            var CinemaUpdated = await _cinemaService.Update(Cinema.Id, Cinema);

            if (CinemaUpdated != null)
            {
                _logger.LogInformation($"Producer update succeded: {DateTime.Now}");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogInformation($"Producer update failed: {DateTime.Now}");
                return View("Empty");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            try
            {
                var Cinema = await _cinemaService.GetById(id);

                return View(Cinema);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogCritical(ex, "Cinema could not be found with message: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return View("Empty");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var Cinema = await _cinemaService.GetById(id);

                Cinema = await _cinemaService.IncludeMovies(Cinema);

                if (Cinema == null)
                {
                    return View("Empty");
                }

                return View(Cinema);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogCritical(ex, "Cinema could not be found with message: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return View("Empty");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete([Bind("Id, PublicId")] Cinema cinema)
        {
            var DeletedCinema = await _cinemaService.Delete(cinema);

            if (DeletedCinema == null)
            {
                return View("Empty");
            }

            if (!string.IsNullOrEmpty(DeletedCinema.PublicId))
            {
                await _photoService.DeletePhotoAsync(DeletedCinema.PublicId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
