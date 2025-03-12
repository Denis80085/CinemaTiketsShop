using CinemaTiketsShop.Services.CognitoUserMenager.Token;

namespace CinemaTiketsShop.Services.CookieService
{
    public interface ICookieRepository
    {
        void SetAuthCookie(TokenModel tokenModel, HttpContext context);
    }
}
