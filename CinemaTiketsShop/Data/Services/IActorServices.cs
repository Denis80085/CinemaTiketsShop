using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;
using System.Threading.Tasks;

namespace CinemaTiketsShop.Data.Services
{
    public interface IActorServices
    {
        Task<IEnumerable<Actor>> GetActors();
        Task<ActorResult> GetById(int id);
        Task Create(Actor actor);
        Task<Actor?> Update(int id, Actor NewActor);
        Task<Actor?> Delete(Actor actor);
    }
}
