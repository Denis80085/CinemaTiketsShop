namespace CinemaTiketsShop.ResponseDtos.UserResponsDtos
{
    public class UserSignUpResponse : BaseResponseModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

    }
}
