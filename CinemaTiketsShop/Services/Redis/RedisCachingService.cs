using CinemaTiketsShop.Data.Base;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CinemaTiketsShop.Services.Redis
{
    public class RedisCachingService : IRedisCachingService
    {
        private readonly IDistributedCache? _cache;

        public RedisCachingService(IDistributedCache? cache)
        {
            _cache = cache;
        }

        public async Task DeleteValues(string Key)
        {
            await _cache!.RemoveAsync(Key);
        }

        public async Task<IEnumerable<T>> GetValues<T>(string Key)
        {
            try
            {

                var StringValues = await _cache!.GetStringAsync(Key);

                var Data = JsonConvert.DeserializeObject<IEnumerable<T>>(StringValues!);

                if (Data == null)
                {
                    return Enumerable.Empty<T>();
                }

                return Data;
            }
            catch 
            {
                return Enumerable.Empty<T>();
            }
        }

        public async Task<T?> Set<T>(string Key, T Value)
        {
            var StringVal = JsonConvert.SerializeObject(Value);

            await _cache!.SetStringAsync(Key, StringVal);

            var SetedData = JsonConvert.DeserializeObject<T>(StringVal);

            return SetedData;
        }

        public async Task<IEnumerable<T>> SetValues<T>(string Key, IEnumerable<T> Values)
        {
            try
            {

                var StringVals = JsonConvert.SerializeObject(Values);

                if (StringVals == null)
                {
                    return Enumerable.Empty<T>();
                }

                await _cache!.SetStringAsync(Key, StringVals);

                var SetedData = JsonConvert.DeserializeObject<IEnumerable<T>>(StringVals);

                if (SetedData == null)
                {
                    return Enumerable.Empty<T>();
                }

                return SetedData;
            }
            catch 
            { 
                return Enumerable.Empty<T>(); 
            }
        }
    }
}
