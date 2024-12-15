using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ProducerResult> GetByIdAsync(int id)
        {
            try 
            {
                var Producer = await _context.Producers.FirstOrDefaultAsync(x => x.Id == id);
                
                if (Producer != null)
                {
                    return ProducerResult.Found(Producer);
                }
                else 
                {
                    Debug.WriteLine($"Producer wiht id {id} not found", category: "Producer not found");
                    return ProducerResult.NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, category: "Producer Service Server Error");
                return ProducerResult.NotFound();
            }
        }

        public async Task<ProducerResult> UpdateAsync(Producer UpdatedProducer, int id)
        {
            try
            {
                if(!_context.Producers.Any(p => p.Id == id)) 
                {
                    throw new KeyNotFoundException($"Producer with id {id} not found");
                }

                _context.Producers.Update(UpdatedProducer);
                await _context.SaveChangesAsync();

                return ProducerResult.UpdateSuccess(UpdatedProducer);
            }
            catch(KeyNotFoundException ex)
            {
                Debug.WriteLine(ex.Message, category: "Producer Service KeyNotFound Error");
                return ProducerResult.UpdateFails();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, category: "Producer Service Updating Error");
                return ProducerResult.UpdateFails();
            }
        }
    }
}
