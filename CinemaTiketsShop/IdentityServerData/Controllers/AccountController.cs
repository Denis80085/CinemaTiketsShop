using Microsoft.AspNetCore.Mvc;

namespace CinemaTiketsShop.IdentityServerData.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {

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
