using CinemaTiketsShop.Services.CognitoUserMenager.Token;

namespace CinemaTiketsShop.Services.CookieService
{
    public interface ICookieRepository
    {
        void DeleteCookie(string key, HttpContext context);
        void SetCookie(string key, string value, HttpContext context);
        void SetAuthCookie(TokenModel tokenModel, HttpContext context);
        public string? GetCookie(string key, HttpContext context);
    }
}
