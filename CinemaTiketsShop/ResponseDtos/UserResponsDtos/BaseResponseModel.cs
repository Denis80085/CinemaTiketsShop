namespace CinemaTiketsShop.ResponseDtos.UserResponsDtos
{
    public abstract class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
