using CinemaTiketsShop.Helpers.EncryptionHelpers.AES;
using CinemaTiketsShop.Models.MessageModels;
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
        private readonly IAES_EcryptionHelper _AesEcrytor;
        public AccountController(IUserRepository userRepository, ICookieRepository cookieRepository, IAES_EcryptionHelper AesEcrytor)
        {
            _userRepository = userRepository;
            _cookieRepository = cookieRepository;
            _AesEcrytor = AesEcrytor;
        }

        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login() 
        {
            return View(new UserLoginModel { Email = "@test@gmail.com", Password = "Password", Message = new WarningMessage("Hi  i am a failure message") });
        }

        [HttpPost]
        [ActionName("Authorize")]
        public async Task<IActionResult> Authorize([Bind("Email, Password")] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("login", userLoginModel);
            }

            UserProfileResponse userProfileResponse = userProfileResponse = await _userRepository.GetUserByEmailAsync(userLoginModel.Email);

            if (userProfileResponse.IsSuccess && (!userProfileResponse.UserProfile!.IsEmailVerified || userProfileResponse.UserProfile.UserStatus.ToLower() == "unconfirmed"))
            {
                var response = await _userRepository.ResendConfirmationCodeAsync( new UserResendConfirmCodeModel 
                { 
                    UserId = userProfileResponse.UserProfile.UserId, 
                    UserName = userProfileResponse.UserProfile.UserName 
                });

                if (!response.IsSuccess) 
                {
                    ModelState.AddModelError("Email", "Invalid login credentials.");
                    return View("login", userLoginModel);
                }

                TempData["UserId"] = _AesEcrytor.Encrypt(userProfileResponse.UserProfile.UserId);
                TempData["UserName"] = _AesEcrytor.Encrypt(userProfileResponse.UserProfile.UserName);

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
        [ActionName("CreateUser")]
        [Route("signup")]
        public async Task<IActionResult> CreateUser([Bind("UserName, Email, Password, GivenName, FamilyName")]UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
            {
                return View("signup", userSignUpModel);
            }

            var result = await _userRepository.CreateUserAsync(userSignUpModel);

            if (result.IsSuccess)
            {
                TempData["UserId"] = _AesEcrytor.Encrypt(result.UserId);
                TempData["UserName"] = _AesEcrytor.Encrypt(result.UserName);

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
            string? userId_encrypted = TempData["UserId"]?.ToString();
            string? userName_encrypted = TempData["UserName"] as string;

            if (string.IsNullOrEmpty(userId_encrypted as string) || string.IsNullOrEmpty(userName_encrypted))
            {
                return View("login");
            }

            string userId = _AesEcrytor.Decrypt(userId_encrypted);
            string userName = _AesEcrytor.Decrypt(userName_encrypted);

            var model = new UserConfirmSignUpModel { UserId = userId, UserName = userName };

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
