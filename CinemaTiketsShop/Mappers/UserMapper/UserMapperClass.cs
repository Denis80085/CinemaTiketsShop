using Amazon.CognitoIdentityProvider.Model;
using CinemaTiketsShop.Models.UserModels;

namespace CinemaTiketsShop.Mappers.UserMapper
{
    public static class UserMapperClass
    {
        public static UserProfileModel? MapUserProfileModel(this UserType FoundUser) 
        {
            string? userId = FoundUser.Attributes.Find(x => x.Name == "sub")?.Value;
            string? userName = FoundUser.Username;
            string? email = FoundUser.Attributes.Find(x => x.Name == "email")?.Value;
            string? givenName = FoundUser.Attributes.Find(x => x.Name == "given_name")?.Value;
            string? familyName = FoundUser.Attributes.Find(x => x.Name == "family_name")?.Value;
            bool isEmailVerified = FoundUser.Attributes.Find(x => x.Name == "email_verified")?.Value == "true" ? true : false;
            string userStatus = FoundUser.UserStatus;

            if ( string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(givenName) || string.IsNullOrEmpty(familyName))
            {
                return null;
            }

            return new UserProfileModel
            {
                UserName = userName,
                Email = email,
                GivenName = givenName,
                FamilyName = familyName,
                IsEmailVerified = isEmailVerified,
                UserId = userId,
                UserStatus = userStatus
            };
        }

    }
}
