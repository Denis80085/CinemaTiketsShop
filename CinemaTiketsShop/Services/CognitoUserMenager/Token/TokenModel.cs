﻿namespace CinemaTiketsShop.Services.CognitoUserMenager.Token
{
    public class TokenModel
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
