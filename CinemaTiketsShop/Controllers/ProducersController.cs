﻿using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.ViewModels.ProducerVMs;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CinemaTiketsShop.Controllers
{
    public class ProducersController : Controller
    {
        private readonly ApplicationDbConntext _context;
        private readonly IProducerService _ProducerService;
        private readonly ILogger _logger;
        private readonly IPhotoService _photoService;

        public ProducersController(ApplicationDbConntext context, IProducerService producerService, ILogger<ProducersController> logger, IPhotoService photoService)
        {
            _context = context;
            _ProducerService = producerService;
            _logger = logger;
            _photoService = photoService;
        }

        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation($"Producer Controler Index called: {DateTime.Now}");

                var Producers = await _context.Producers.Select(p => p).ToListAsync();

                return View(Producers);
            }
            catch (Exception ex) 
            {
                _logger.LogCritical(ex, "Producer Controler Error at Index");

                return View("Empty");
            }           
        }

        public IActionResult Create() 
        {
            _logger.LogInformation($"Producer Controler Create-View called: {DateTime.Now}");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Bio, Foto, PictureUrl")]CreateProducerViewModel ProducerVM) 
        {
            _logger.LogInformation($"Producer Controler Create-Action called: {DateTime.Now}");

            if (ProducerVM.Foto == null && string.IsNullOrWhiteSpace(ProducerVM.PictureUrl))
            {
                ModelState.AddModelError("Foto", "No picture was found");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = new ImageUploadResult();

                    if(ProducerVM.Foto != null) 
                    {
                        result = await _photoService.UploadPhotoAsync(ProducerVM.Foto);
                    }
                    else if(!string.IsNullOrWhiteSpace(ProducerVM.PictureUrl)) 
                    {
                        if (PictureUrl.isValid(ProducerVM.PictureUrl)) 
                        {
                            result = await _photoService.UploadPhotoWithUrlAsync(ProducerVM.PictureUrl);
                        }
                        else 
                        {
                            ModelState.AddModelError("PictureUrl", "Url doesent point to a image of type .jpg, .png, .webp or .svg");
                            return View(ProducerVM);
                        }
                        
                    }                    

                    var NewProducer = new Producer
                    {
                        Name = ProducerVM.Name,
                        Bio = ProducerVM.Bio,
                        FotoURL = result.Url.ToString()
                    };

                    var producer = await _ProducerService.CreateAsync(NewProducer);

                    if (producer != null)
                    {
                        _logger.LogInformation($"Producer created: {producer.Name}, {DateTime.Now}");

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw new ArgumentNullException("Producer could not be created");
                    }
                }
                catch (ArgumentNullException ex)
                {
                    _logger.LogError(ex, $"Producer Controller Error at creating, {DateTime.Now}");

                    return View("Empty");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Producer Controller Error at creating, {DateTime.Now}");

                    return View("Empty");
                }
            }
            else
            {
                _logger.LogWarning($"Producer Controler Create-Action validation failed: {DateTime.Now}");

                return View(ProducerVM);
            }   
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            _logger.LogInformation($"Producer Controler Edit-View called: {DateTime.Now}");

            try 
            {
                var ProducerResult = await _ProducerService.GetByIdAsync(id);

                if (ProducerResult.isFound) 
                {
                    return View(ProducerResult.Producer);
                }
                else 
                {
                    throw new ArgumentNullException($"Producer with id {id} not found");
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, $"Producer Controller Error at Editing, {DateTime.Now}");

                return View("Empty");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Producer Controller Error at Editing, {DateTime.Now}");

                return RedirectToAction(nameof(Index)); //ToDo: replace with Empty Page, Add logging 
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]int Id, [Bind("Id, Name, Bio, FotoURL")] Producer UpdatedProducer)
        {
            _logger.LogInformation($"Producer Controler Edit-Action called: {DateTime.Now}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Producer Controler Edit-Action validation failed: {DateTime.Now}");
                return View(UpdatedProducer);
            }

            try
            {
                var ProducerResult = await _ProducerService.UpdateAsync(UpdatedProducer, Id);

                if (ProducerResult.UpdateSucceded)
                {
                    _logger.LogInformation($"Producer update succeded: {DateTime.Now}");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception($"Update of producer failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Producer Update Failed, {DateTime.Now}");

                return RedirectToAction(nameof(Index)); //ToDo: replace with Empty Page, Add logging 
            }
        }

        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            _logger.LogInformation($"Producer Controler Delete-View called: {DateTime.Now}");
            try
            {
                var producerResult = await _ProducerService.GetByIdAsync(id);

                if (producerResult.isFound) 
                {
                    return View(producerResult.Producer);
                }
                else 
                {
                    throw new Exception($"Deleting of producer failed. Producer Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Producer-Delete Failed, {DateTime.Now}");

                return RedirectToAction(nameof(Index));//ToDo: replace with Empty Page, Add logging
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromForm] int Id) 
        {
            _logger.LogInformation($"Producer Controler Delete-Action called: {DateTime.Now}");
            try 
            {
                var producerResult = await _ProducerService.GetByIdAsync(Id);

                if (producerResult.isFound && producerResult.Producer !=  null)
                {
                    await _ProducerService.DeleteAsync(producerResult.Producer);

                    _logger.LogWarning($"Producer {producerResult.Producer.Name} was removed from database: {DateTime.Now}");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception($"Confirm Deleting of the producer failed. Producer Not Found");
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, $"Producer-Delete Failed, {DateTime.Now}");

                return RedirectToAction(nameof(Index));//ToDo: replace with Empty Page, Add logging
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details([FromRoute]int id) 
        {
            _logger.LogInformation($"Details-View called with id {id}");

            try 
            {
                var producerResult = await _ProducerService.GetByIdAsync(id);
                
                if (producerResult.isFound && producerResult.Producer != null)
                {
                    return View(producerResult.Producer);
                }
                else
                { 
                    throw new ArgumentNullException($"Producer with id {id} Not Found");
                }
            }
            catch(ArgumentNullException ex) 
            {
                _logger.LogError(ex, $"Producer-Details Null Exception, {DateTime.Now}");
                return View("Empty");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Producer-Details Failed, {DateTime.Now}");
                return View("Empty");
            }
        }
    }
}
