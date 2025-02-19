using CinemaTiketsShop.Data.Base;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Collections;

namespace CinemaTiketsShop.Services.Redis
{
    public class RedisCachingService : IRedisCachingService
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly ILogger<RedisCachingService> _logger;

        public RedisCachingService(IConnectionMultiplexer connection, ILogger<RedisCachingService> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task DeleteValues(string Key)
        {
            var db = _connection.GetDatabase();

            await db.StringGetDeleteAsync(Key);
        }

        public async Task<IEnumerable<T>> GetValues<T>(string Key)
        {
            try
            {
                var db = _connection.GetDatabase();

                var StringValues = await db.StringGetAsync(Key);

                var Data = JsonConvert.DeserializeObject<IEnumerable<T>>(StringValues.ToString());

                if (Data == null)
                {
                    return Enumerable.Empty<T>();
                }

                return Data;
            }
            catch(Exception e) 
            {
                _logger.LogError(e.ToString());
                return Enumerable.Empty<T>();
            }
        }


        public async Task<IEnumerable<T>> SetValues<T>(string Key, IEnumerable<T> Values, long TTL_Min)
        {
            try
            {
                var db = _connection.GetDatabase();

                var StringVals = JsonConvert.SerializeObject(Values);

                if (string.IsNullOrWhiteSpace(StringVals))
                {
                    return Enumerable.Empty<T>();
                }

                var randomOffset = TimeSpan.FromSeconds(new Random().Next(1, 5));

                await db.KeyDeleteAsync(Key);
                await db.StringSetAsync(Key, StringVals, expiry: TimeSpan.FromMinutes(TTL_Min) + randomOffset);

                var SetedData = JsonConvert.DeserializeObject<IEnumerable<T>>(StringVals);

                if (SetedData == null)
                {
                    return Enumerable.Empty<T>();
                }

                return SetedData;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return Enumerable.Empty<T>(); 
            }
        }
    }
}
