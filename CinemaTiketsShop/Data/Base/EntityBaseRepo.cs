
using CinemaTiketsShop.Services.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CinemaTiketsShop.Data.Base
{
    public class EntityBaseRepo<T> : IEntityBaseRepo<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbConntext _context;
        private readonly IRedisCachingService _cache;
        private readonly string cache_key;

        public EntityBaseRepo(ApplicationDbConntext context, IRedisCachingService cache, string CacheKey)
        {
            _context = context;
            _cache = cache;
            cache_key = CacheKey;
        }

        virtual public async Task<T?> Create(T entity)
        {
            var entry = await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        virtual public async Task<T?> Delete(T entity)
        {
            var Entity = _context.Set<T>().Remove(entity);

            if (Entity.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return entity;
            }
            else
                return null;
        }

        virtual public async Task<IEnumerable<T>> GetAll()
        {

            var entities = await _cache.GetValues<T>(cache_key);

            if (entities.Any())
            {
                return entities;
            }

            entities = await _context.Set<T>().ToListAsync();

            await _cache.SetValues(cache_key, entities, 5);

            return entities;
        }

        virtual public async Task<T> GetById(int id)
        {
            var foundEntity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            if (foundEntity == null)
            {
                throw new KeyNotFoundException($"{nameof(T)} with id {id} could not be found");
            }

            return foundEntity;
        }

        virtual async public Task<T?> Update(int id, T entity)
        {
            if(!_context.Set<T>().Any(e => e.Id == id)) 
            {
                return null;
            }

            var updatedEntity = _context.Set<T>().Update(entity);

            if(updatedEntity.State == EntityState.Modified) 
            {
                await _context.SaveChangesAsync();
                return entity;
            }
            else 
                return null;
        }
    }
}
