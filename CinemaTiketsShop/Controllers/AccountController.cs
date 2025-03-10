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
        public async Task<IActionResult> Register([FromBody] UserLoginModel registerUserDto)
        {
            var result = await _userRepository.TryLoginAsync(registerUserDto);

            if(result.IsSuccess)
            {
                return Ok(result);
            }
            else 
            {
                return Unauthorized(result);
            }
        }
    }
}
