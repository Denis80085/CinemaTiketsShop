using CinemaTiketsShop.Services.CognitoUserMenager.Token;

namespace CinemaTiketsShop.ResponseDtos.UserResponsDtos
{
    public class AuthResponseModel : BaseResponseModel
    {
        public TokenModel Tokens { get; set; } = new();
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
