using CinemaTiketsShop.Models.UserModels;
using CinemaTiketsShop.Services.CognitoUserMenager;
using CinemaTiketsShop.Services.CookieService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTiketsShop.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICookieRepository _cookieRepository;
        public AccountController(IUserRepository userRepository, ICookieRepository cookieRepository)
        {
            _userRepository = userRepository;
            _cookieRepository = cookieRepository;
        }

        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authorize([Bind("Email, Password")] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("login", userLoginModel);
            }

            var result = await _userRepository.TryLoginAsync(userLoginModel);

            if (result.IsSuccess)
            {
                _cookieRepository.SetAuthCookie(result.Tokens, HttpContext);
                return RedirectToAction("profile");
            }
            else
            {
                ModelState.AddModelError("Email", result.Message);
                return View("login", userLoginModel);
            }
        }

        //[HttpPost]
        //[Route("signup")]
        //public async Task<IActionResult> SignUp([FromBody] UserSignUpModel userSignUpModel)
        //{
        //    var result = await _userRepository.CreateUserAsync(userSignUpModel);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}

        [Authorize]
        [Route("profile")]
        public IActionResult UserProfile()
        {
            return View("profile");
        }
    }
}
