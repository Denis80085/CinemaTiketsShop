using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public interface IProducerService
    {
        Task<Producer?> CreateAsync(Producer NewProducer);
    }
}
