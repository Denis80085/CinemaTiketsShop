using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;
using System.Threading.Tasks;

namespace CinemaTiketsShop.Data.Services
{
    public interface IActorServices : IEntityBaseRepo<Actor>
    {
        //Task<IEnumerable<Actor>> GetActors();
        Task<ActorResult> GetActorResultById(int id);
        
    }
}
