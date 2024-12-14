using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaTiketsShop.Controllers
{
    public class ProducersController : Controller
    {
        private readonly ApplicationDbConntext _context;
        private readonly IProducerService _ProducerService;

        public ProducersController(ApplicationDbConntext context, IProducerService producerService)
        {
            _context = context;
            _ProducerService = producerService;
        }

        public  async Task<IActionResult> Index()
        {
            var Producers = await _context.Producers.Select(p => p).ToListAsync();

            return View(Producers);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Bio, FotoURL")]Producer NewProducer) 
        {
            if (!ModelState.IsValid) 
            {
                return View(NewProducer);
            }

            try
            {
                var producer = await _ProducerService.CreateAsync(NewProducer);
                
                if(producer != null) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    throw new ArgumentNullException("Producer could not be created");
                }
            }
            catch(ArgumentNullException ex) 
            {
                Debug.WriteLine(ex.Message, category: "Producer NullException at creating");

                return View(NewProducer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, category: "Producer creating error");
                return View(NewProducer);
            }
        }
    }
}
