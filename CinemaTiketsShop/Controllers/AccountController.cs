using CinemaTiketsShop.Models.UserModels;
using CinemaTiketsShop.ResponseDtos.UserResponsDtos;
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

            var UserProfileResponse = await _userRepository.GetUserByEmailAsync(userLoginModel.Email);

            if (UserProfileResponse.IsSuccess && (!UserProfileResponse.UserProfile!.IsEmailVerified || UserProfileResponse.UserProfile.UserStatus.ToLower() == "unconfirmed"))
            {
                var response = await _userRepository.ResendConfirmationCodeAsync( new UserResendConfirmCodeModel 
                { 
                    UserId = UserProfileResponse.UserProfile.UserId, 
                    UserName = UserProfileResponse.UserProfile.UserName 
                });

                if (!response.IsSuccess) 
                {
                    ModelState.AddModelError("Email", "Invalid login credentials.");
                    return View("login", userLoginModel);
                }

                TempData["UserId"] = UserProfileResponse.UserProfile.UserId;
                TempData["UserName"] = UserProfileResponse.UserProfile.UserName;

                return RedirectToAction(nameof(ConfirmEmail));
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
                TempData["UserId"] = (string)result.UserId;
                TempData["UserName"] = (string)result.UserName;

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
            string? userId = TempData["UserId"]?.ToString();
            string? userName = TempData["UserName"] as string;

            if (string.IsNullOrEmpty(userId  as string) || string.IsNullOrEmpty(userName))
            {
                return View("login");
            }

            var model = new UserConfirmSignUpModel { UserId = userId as string, UserName = userName };

            return View(model);
        }

        [Route("tryconfirm")]
        [HttpPost]
        public async Task<IActionResult> TryConfirmUser(UserConfirmSignUpModel model)
        {
            
            var result = await _userRepository.ConfirmUserSignUpAsync(model);

            if (result.IsSuccess) 
            {
                return View("login");
            }

            await _userRepository.ResendConfirmationCodeAsync(new UserResendConfirmCodeModel
            {
                UserId = model.UserId,
                UserName = model.UserName
            });

            return View("confirmemail");
        }

        [Authorize]
        [Route("profile")]
        public IActionResult UserProfile()
        {
            return View("profile");
        }
    }
}
