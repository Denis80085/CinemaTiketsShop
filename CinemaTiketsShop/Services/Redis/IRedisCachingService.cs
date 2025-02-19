using CinemaTiketsShop.Data.Base;

namespace CinemaTiketsShop.Services.Redis
{
    public interface IRedisCachingService
    {
        Task<IEnumerable<T>> GetValues<T>(string Key);
        Task<IEnumerable<T>> SetValues<T>(string Key, IEnumerable<T> Values, long TTL_Min);
        Task DeleteValues(string Key);
    }
}
