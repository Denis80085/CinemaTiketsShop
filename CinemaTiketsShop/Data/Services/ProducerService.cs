using CinemaTiketsShop.Models;
using System.Diagnostics;

namespace CinemaTiketsShop.Data.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ApplicationDbConntext _context;

        public ProducerService(ApplicationDbConntext context) 
        {
            _context = context;
        }

        public async Task<Producer?> CreateAsync(Producer NewProducer)
        {
            try 
            {
                await _context.Producers.AddAsync(NewProducer);
                await _context.SaveChangesAsync();

                return NewProducer;
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.Message, category: "Producer Creating Error");
                return null;
            }
        }
    }
}
