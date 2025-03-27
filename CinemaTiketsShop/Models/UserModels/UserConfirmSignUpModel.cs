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
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        public MessageModel? Message { get; set; }
    }
}
