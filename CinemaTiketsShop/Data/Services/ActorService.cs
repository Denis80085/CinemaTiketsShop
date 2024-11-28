using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Data.Services
{
    public class ActorService : IActorServices
    {
        private readonly ApplicationDbConntext _context;

        public ActorService(ApplicationDbConntext context)
        {
            _context = context;
        }

        public Task Create(Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Delete(Actor actor)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            var result = await _context.Actors.Select(a => a).ToListAsync();

            return result;
        }

        public async Task<ActorResult> GetById(int id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (result != null) 
            {
                return ActorResult.Found(result);
            }
            else 
            {
                return ActorResult.NotFound();
            }
        }

        public Task<Actor> Update(int id, Actor NewActor)
        {
            throw new NotImplementedException();
        }
    }
}
