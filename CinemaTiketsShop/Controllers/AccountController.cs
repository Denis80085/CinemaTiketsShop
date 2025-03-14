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

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([Bind("UserName, Email, Password, GivenName, FamilyName")]UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
            {
                return View("signup", userSignUpModel);
            }

            var result = await _userRepository.CreateUserAsync(userSignUpModel);

            if (result.IsSuccess)
            {
                _cookieRepository.SetCookie("email", result.Email, HttpContext);
                _cookieRepository.SetCookie("uid", result.UserId, HttpContext);
                return RedirectToAction(nameof(ConfirmEmail));
            }
            else
            {
                ModelState.AddModelError("Email", result.Message);
                return View("signup", userSignUpModel);
            }
        }

        [Route("confirmemail")]
        public IActionResult ConfirmEmail() 
        {
            return View();
        }

        [Route("tryconfirm")]
        [HttpPost]
        public async Task<IActionResult> TryConfirmUser([Bind("ConfirmCode")]UserConfirmSignUpModel userConfirmSignUpModel)
        {
            HttpContext.Request.Cookies.TryGetValue("uid", out var uid);
            HttpContext.Request.Cookies.TryGetValue("email", out var email);

            userConfirmSignUpModel.Email = email;
            userConfirmSignUpModel.UserId = uid;

            var result = await _userRepository.ConfirmUserSignUpAsync(userConfirmSignUpModel);

            if (result.IsSuccess) 
            {
                return View("login");
            }

            return View("confirmemail", userConfirmSignUpModel);
        }

        [Authorize]
        [Route("profile")]
        public IActionResult UserProfile()
        {
            return View("profile");
        }
    }
}
