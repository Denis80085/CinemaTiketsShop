using CinemaTiketsShop.Services.CognitoUserMenager.Token;

namespace CinemaTiketsShop.Services.CookieService
{
    public class CookieRepository : ICookieRepository
    {
        public void SetAuthCookie(TokenModel tokenModel, HttpContext context)
        {
            context.Response.Cookies.Append("access_token", tokenModel.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(tokenModel.ExpiresIn)
            });

            context.Response.Cookies.Append("refresh_token", tokenModel.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Expires = DateTimeOffset.UtcNow.AddDays(5)
            });
        }
    }
}
