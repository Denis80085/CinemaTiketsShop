using CinemaTiketsShop.Data.Base;

namespace CinemaTiketsShop.Services.Redis
{
    public interface IRedisCachingService
    {
        Task<IEnumerable<T>> GetValues<T>(string Key) where T : class, IEntityBase; 
        Task SetValues<T>(string Key, IEnumerable<T> Values, long TTL_Min) where T : class, IEntityBase;
        Task DeleteValues(string Key);

        Task<T?> GetVal<T>(string Key, int id) where T : class, IEntityBase;
        Task SetVal<T>(string Key, T Val, long TTL_Min) where T : class, IEntityBase;
    }
}
