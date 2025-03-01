using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.ActorVMs;

namespace CinemaTiketsShop.Mappers.ActorMappers
{
    public static class EditActorMapper
    {
        public static EditActorViewModel MapEditActorViewModel(this Actor ActorModel)
        {
            return new EditActorViewModel
            {
                Id = ActorModel.Id,
                Name = ActorModel.Name,
                Bio = ActorModel.Bio,
                PublicId = ActorModel.PublicId,
                OldFotoUrl = ActorModel.FotoURL,
                FotoUrl = ActorModel.FotoURL
            };
        }

        public static Actor MapActorModel(this EditActorViewModel ActorVM)
        {
            return new Actor
            {
                Id = ActorVM.Id,
                Name = ActorVM.Name,
                Bio = ActorVM.Bio,
                PublicId = ActorVM.PublicId,
                FotoURL = ActorVM.OldFotoUrl
            };
        }
    }
}
