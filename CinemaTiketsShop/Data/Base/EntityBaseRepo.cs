﻿
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Data.Base
{
    public class EntityBaseRepo<T> : IEntityBaseRepo<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbConntext _context;

        public EntityBaseRepo(ApplicationDbConntext context)
        {
            _context = context;
        }

        virtual public Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        virtual public Task<T?> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        virtual public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
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

        virtual public Task<T?> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
