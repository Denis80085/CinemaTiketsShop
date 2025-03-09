using CinemaTiketsShop.IdentityServerData.Dtos;
using CinemaTiketsShop.IdentityServerData.Models;
using CinemaTiketsShop.IdentityServerData.Results;
using CinemaTiketsShop.IdentityServerData.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CinemaTiketsShop.IdentityServerData.Services.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResult> RegisterUser(User NewUser, string Password)
        {
            var result = await _userManager.CreateAsync(NewUser, Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(NewUser, "Costumer");

                return new RegisterResult { Succeeded = true };
            }
            else
            {
                var errors = new Queue<string>();
                foreach (var error in result.Errors)
                {
                    errors.Enqueue(error.Description);
                }
                return new RegisterResult { Succeeded = false, Errors = errors };
            }
        }
    }
}
