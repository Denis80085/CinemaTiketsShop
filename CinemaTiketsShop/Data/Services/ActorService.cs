using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;

namespace CinemaTiketsShop.Data.Services
{
    public class ActorService : IActorServices
    {
        private readonly ApplicationDbConntext _context;

        public ActorService(ApplicationDbConntext context)
        {
            _context = context;
        }

        public async Task Create(Actor actor)
        {
            await _context.Actors.AddAsync(actor);

            await _context.SaveChangesAsync();
        }

        public async Task<Actor?> Delete(Actor actor)
        {
            try
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();

                return actor;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message, category: "Actor Updating Error");
                return null;
            }

        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            var result = await _context.Actors.Select(a => a).ToListAsync();

            return result;
        }

        public async Task<ActorResult> GetById(int id)
        {
            var Actors = await _context.Actors.Include(a => a.Movies_Actors).ThenInclude(a => a.Movie).Where(a => a.Id == id ).Select(a => a).ToListAsync();

            if (Actors != null) 
            {
                return ActorResult.Found(Actors[0]);
            }
            else 
            {
                return ActorResult.NotFound();
            }
        }

        public async Task<Actor?> Update(int id, Actor NewActor)
        {
            try
            {
                if(_context.Actors.Any(a => a.Id == id)) 
                {
                    _context.Update(NewActor);

                    await _context.SaveChangesAsync();

                    return NewActor;
                }
                else 
                {
                    throw new KeyNotFoundException($"No Actor with id {id} was found");
                }
                
            }
            catch (KeyNotFoundException kex)
            {
                Debug.WriteLine(kex.Message, category: "Actor Id not found while Updating");
                return null;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message, category: "Actor Updating Error");
                return null;
            }  
        }
    }
}
