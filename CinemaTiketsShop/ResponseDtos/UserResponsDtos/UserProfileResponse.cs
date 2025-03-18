using CinemaTiketsShop.Models.UserModels;

namespace CinemaTiketsShop.ResponseDtos.UserResponsDtos
{
    public class UserProfileResponse :BaseResponseModel
    {
        public UserProfileModel? UserProfile { get; set; }
    }
}
