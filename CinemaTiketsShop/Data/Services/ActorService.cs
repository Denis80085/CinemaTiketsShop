using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;
using CinemaTiketsShop.Services.Redis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;

namespace CinemaTiketsShop.Data.Services
{
    public class ActorService : EntityBaseRepo<Actor>, IActorServices
    {
        private readonly ApplicationDbConntext _context;

        public ActorService(ApplicationDbConntext context, IRedisCachingService cache) : base(context, cache, "Actor") 
        {
            _context = context;
        }

        public override async Task<Actor?> Create(Actor actor)
        {
            await _context.Actors.AddAsync(actor);

            await _context.SaveChangesAsync();

            return actor;
        }

        public override async Task<Actor?> Delete(Actor actor)
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

        //public async Task<IEnumerable<Actor>> GetActors()
        //{
        //    var result = await _context.Actors.Select(a => a).ToListAsync();

        //    return result;
        //}

        public  async Task<ActorResult> GetActorResultById(int id)
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

        //public override async Task<Actor?> Update(int id, Actor NewActor)
        //{
        //    try
        //    {
        //        if(_context.Actors.Any(a => a.Id == id)) 
        //        {
        //            _context.Update(NewActor);

        //            await _context.SaveChangesAsync();

        //            return NewActor;
        //        }
        //        else 
        //        {
        //            throw new KeyNotFoundException($"No Actor with id {id} was found");
        //        }
                
        //    }
        //    catch (KeyNotFoundException kex)
        //    {
        //        Debug.WriteLine(kex.Message, category: "Actor Id not found while Updating");
        //        return null;
        //    }
        //    catch (Exception ex) 
        //    {
        //        Debug.WriteLine(ex.Message, category: "Actor Updating Error");
        //        return null;
        //    }  
        //}

        

        
    }
}
