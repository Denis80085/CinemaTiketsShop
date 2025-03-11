using CinemaTiketsShop.Models.UserModels;
using CinemaTiketsShop.Services.CognitoUserMenager;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTiketsShop.Controllers
{
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            var result = await _userRepository.TryLoginAsync(userLoginModel);

            if(result.IsSuccess)
            {
                return Ok(result);
            }
            else 
            {
                return Unauthorized(result);
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpModel userSignUpModel)
        {
            var result = await _userRepository.CreateUserAsync(userSignUpModel);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
