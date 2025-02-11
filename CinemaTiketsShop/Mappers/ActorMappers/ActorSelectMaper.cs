using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.BaseAbstractVMs;

namespace CinemaTiketsShop.Mappers.ActorMappers
{
    public static class ActorSelectMaper
    {
        public static ItemSelect MapActorsSelect(this Actor Model) 
        {
            return new ItemSelect
            { 
                Id = Model.Id,
                Name = Model.Name,
                Picture = Model.FotoURL
            };
        }
    }
}
