using CinemaTiketsShop.Models.MessageModels;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.Models.UserModels
{
    public class UserConfirmSignUpModel
    {
        [Required]
        public string ConfirmCode { get; set; } = string.Empty;
        [Required]
<<<<<<< HEAD
        public string UserId { get; }
        [Required]
        public string UserName { get; }
        public MessageModel? Message { get; set; }
        public int ConfirmationFailed { get; private set; } = 0;
        public int MaxConfirmationFailed { get; }

        public UserConfirmSignUpModel(int MaxTries,  string UserId, string UserName)
        {
            MaxConfirmationFailed = MaxTries;
            this.UserId = UserId;
            this.UserName = UserName;
        }
=======
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        public MessageModel? Message { get; set; }
>>>>>>> 928aa27 (RedirectToAction has also messages)
    }
}
