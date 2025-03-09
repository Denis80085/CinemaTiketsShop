using CinemaTiketsShop.IdentityServerData.Dtos;
using CinemaTiketsShop.IdentityServerData.Models;
using CinemaTiketsShop.IdentityServerData.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTiketsShop.IdentityServerData.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult UserProfile() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto) 
        {
            if (!ModelState.IsValid) 
            {
                return View(registerDto);
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email
            };

            var result = await _accountService.RegisterUser(user, registerDto.Password);

            return View(registerDto);
        } 

        public IActionResult Login()
        {
            return View();
        }

 
        public IActionResult Register()
        {
            return View();
        }
    }
}
