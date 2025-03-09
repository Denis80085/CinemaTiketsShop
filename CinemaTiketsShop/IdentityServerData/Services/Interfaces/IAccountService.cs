using CinemaTiketsShop.IdentityServerData.Dtos;
using CinemaTiketsShop.IdentityServerData.Models;
using CinemaTiketsShop.IdentityServerData.Results;

namespace CinemaTiketsShop.IdentityServerData.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterUser(User NewUser, string Password);
    }
}
