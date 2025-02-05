using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public interface IProducerService : IEntityBaseRepo<Producer>
    {
        Task<Producer?> CreateAsync(Producer NewProducer);

        Task<ProducerResult> GetByIdAsync(int id);

        Task<ProducerResult> UpdateAsync(Producer UpdatedProducer, int id);

        Task DeleteAsync(Producer producer);
    }
}
