using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Data.Wrappers;
using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public interface IActorServices : IEntityBaseRepo<Actor>
    {
        //Task<IEnumerable<Actor>> GetActors();
        Task<ActorResult> GetActorResultById(int id);

    }
}
