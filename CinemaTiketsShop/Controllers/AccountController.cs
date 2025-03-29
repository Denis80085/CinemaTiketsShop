using CinemaTiketsShop.Helpers.EncryptionHelpers.AES;
using CinemaTiketsShop.Helpers.MessageProviderHellper;
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
            return View();
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
                    this.SendWarningMessageToView("Invalid login credentials.");
                    return View("login", userLoginModel);
                }

                TempData["UserId"] = _AesEcrytor.Encrypt(userProfileResponse.UserProfile.UserId);
                TempData["UserName"] = _AesEcrytor.Encrypt(userProfileResponse.UserProfile.UserName);

                this.SendWarningMessageToAction("Your account exists but it is unconfirmed. Please confirm your account via email.");
                return RedirectToAction(nameof(ConfirmEmail));
            }

            var result = await _userRepository.TryLoginAsync(userLoginModel);

            if (result.IsSuccess)
            {
                _cookieRepository.SetAuthCookie(result.Tokens, HttpContext);
                this.SendSuccessMessageToView("You have successfully logged in.");
                return View("profile");
            }
            else
            {
                this.SendFailureMessageToView("Invalid login credentials.");
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

            var userByEmail = await _userRepository.GetUserByEmailAsync(userSignUpModel.Email);

            if(userByEmail.IsSuccess)
            {
                this.SendFailureMessageToView("The email is already in use.");
                return View("signup", userSignUpModel);
            }

            var result = await _userRepository.CreateUserAsync(userSignUpModel);

            if (result.IsSuccess)
            {
                TempData["UserId"] = _AesEcrytor.Encrypt(result.UserId);
                TempData["UserName"] = _AesEcrytor.Encrypt(result.UserName);

                _cookieRepository.SetCookie("Email", _AesEcrytor.Encrypt(userSignUpModel.Email), HttpContext);
                _cookieRepository.SetCookie("Password", _AesEcrytor.Encrypt(userSignUpModel.Password), HttpContext);

                this.SendSuccessMessageToAction("Your account was created. Please confirm your account via email.");

                return RedirectToAction(nameof(ConfirmCreatedUserView));
            }
            else
            {
                this.SendFailureMessageToView(result.Message);
                return View("signup", userSignUpModel);
            } // TO DO: Create Diferent Confirmation Page
        }

        public IActionResult ConfirmCreatedUserView()
        {
            string? userId_encrypted = TempData["UserId"]?.ToString();
            string? userName_encrypted = TempData["UserName"] as string;

            if (string.IsNullOrEmpty(userId_encrypted as string) || string.IsNullOrEmpty(userName_encrypted))
            {
                _cookieRepository.DeleteCookie("Email", HttpContext);
                _cookieRepository.DeleteCookie("Password", HttpContext);

                this.SendFailureMessageToView("The confirmation failed");
                return View("signup");
            }

            var model = new UserConfirmSignUpModel { UserId = userId_encrypted, UserName = userName_encrypted };

            return View(model);
        }

        [Route("TryConfirmNewUser")]
        [HttpPost]
        public async Task<IActionResult> TryConfirmNewUser(UserConfirmSignUpModel model)
        {
            model.UserName = _AesEcrytor.Decrypt(model.UserName);
            model.UserId = _AesEcrytor.Decrypt(model.UserId);

            string? email = _cookieRepository.GetCookie("Email", HttpContext);
            string? password = _cookieRepository.GetCookie("Password", HttpContext);

            var result = await _userRepository.ConfirmUserSignUpAsync(model);

            if (result.IsSuccess)
            {
                if(email != null && password != null)
                {
                    var loginModel = new UserLoginModel { Email = _AesEcrytor.Decrypt(email), Password = _AesEcrytor.Decrypt(password) };

                    _cookieRepository.DeleteCookie("Email", HttpContext);
                    _cookieRepository.DeleteCookie("Password", HttpContext);

                    var AuthResult = await _userRepository.TryLoginAsync(loginModel);

                    if (result.IsSuccess)
                    {
                        _cookieRepository.SetAuthCookie(AuthResult.Tokens, HttpContext);
                        this.SendSuccessMessageToAction("Your account was succesfully created.");
                        return RedirectToAction(nameof(UserProfile));
                    }
                    else
                    {
                        this.SendWarningMessageToAction("Please log in again.");
                        return RedirectToAction(nameof(Login), loginModel);
                    }
                }
                else
                {
                    this.SendSuccessMessageToAction("Your email was confirmed. Please log in again.");
                    return RedirectToAction("login");
                }
            }

            await _userRepository.ResendConfirmationCodeAsync(new UserResendConfirmCodeModel
            {
                UserId = model.UserId,
                UserName = model.UserName
            });

            this.SendFailureMessageToView("The confirmation code is wrong");

            model.UserName = _AesEcrytor.Encrypt(model.UserName);
            model.UserId = _AesEcrytor.Encrypt(model.UserId);

            return View("ConfirmCreatedUserView");
        }


        [Route("confirmemail")] // Confirm Email if user is not confirmed
        public IActionResult ConfirmEmail()
        {
            //this.SendWarningMessageToView("Your account exists but it is unconfirmed. Please confirm your account via email.");

            string? userId_encrypted = TempData["UserId"]?.ToString();
            string? userName_encrypted = TempData["UserName"] as string;

            if (string.IsNullOrEmpty(userId_encrypted as string) || string.IsNullOrEmpty(userName_encrypted))
            {
                this.SendFailureMessageToView("The confirmation failed");
                return View("login");
            }

            var model = new UserConfirmSignUpModel { UserId = userId_encrypted, UserName = userName_encrypted };

            return View(model);
        }

        [Route("tryconfirm")] // Confirm Email if user is not confirmed
        [HttpPost]
        public async Task<IActionResult> TryConfirmUser(UserConfirmSignUpModel model)
        {
            model.UserName = _AesEcrytor.Decrypt(model.UserName);
            model.UserId = _AesEcrytor.Decrypt(model.UserId);

            var result = await _userRepository.ConfirmUserSignUpAsync(model);

            if (result.IsSuccess) 
            {
                this.SendSuccessMessageToAction("Your email was confirmed. Please log in again.");
                return RedirectToAction("login");
            }

            await _userRepository.ResendConfirmationCodeAsync(new UserResendConfirmCodeModel
            {
                UserId = model.UserId,
                UserName = model.UserName
            });

            this.SendFailureMessageToView("The confirmation code is wrong");

            model.UserName = _AesEcrytor.Encrypt(model.UserName);
            model.UserId = _AesEcrytor.Encrypt(model.UserId);

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
