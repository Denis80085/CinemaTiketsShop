using CinemaTiketsShop.Data.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
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

        public async Task<T?> GetVal<T>(string Key, int id) where T : class, IEntityBase
        {
            try
            {
                var db = _connection.GetDatabase();

                var Val = await db.HashGetAsync(Key, id);

                if (string.IsNullOrEmpty(Val)) return null;

                var entry = JsonConvert.DeserializeObject<T?>(Val!);

                return entry;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetValues<T>(string Key) where T : class, IEntityBase
        {
            try
            {
                var db = _connection.GetDatabase();

                var Vals = await db.HashGetAllAsync(Key);

                if(!(Vals.Length > 0)) 
                {
                    return Enumerable.Empty<T>();
                }

                IEnumerable<T?> Entries = Vals.Select(x => JsonConvert.DeserializeObject<T>(x.Value!)).ToList();

                if (!Entries.Any())
                {
                    return Enumerable.Empty<T>();
                }

                return Entries!;
            }
            catch(Exception e) 
            {
                _logger.LogError(e.ToString());
                return Enumerable.Empty<T>();
            }
        }

        public async Task SetVal<T>(string Key, T Val, long TTL_Min) where T : class, IEntityBase
        {
            try
            {
                var db = _connection.GetDatabase();

                if (!await db.HashExistsAsync(Key, Val.Id)) 
                    return;

                await db.HashSetAsync(Key, Val.Id, JsonConvert.SerializeObject(Val));

                var expiration = TimeSpan.FromMinutes(TTL_Min) + TimeSpan.FromSeconds(new Random().Next(1, 3));
                await db.KeyExpireAsync(Key, expiration);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }

        public async Task SetValues<T>(string Key, IEnumerable<T> Values, long TTL_Min) where T : class, IEntityBase
        {
            try
            {
                var db = _connection.GetDatabase();

                foreach(var val in Values) 
                {
                    var serializedVal = JsonConvert.SerializeObject(val);

                    await db.HashSetAsync(Key, val.Id, serializedVal.ToString());
                }
                
                var expiration = TimeSpan.FromMinutes(TTL_Min) + TimeSpan.FromSeconds(new Random().Next(1, 3));
                await db.KeyExpireAsync(Key, expiration);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
    }
}
