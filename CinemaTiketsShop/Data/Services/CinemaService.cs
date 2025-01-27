using CinemaTiketsShop.Data.Base;
using CinemaTiketsShop.Models;

namespace CinemaTiketsShop.Data.Services
{
    public class CinemaService : EntityBaseRepo<Cinema>, ICinemaService
    {
        public CinemaService(ApplicationDbConntext context) : base(context)
        {
            
        }
    }
}
