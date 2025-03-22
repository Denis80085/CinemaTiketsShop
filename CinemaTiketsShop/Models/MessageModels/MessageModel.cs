using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CinemaTiketsShop.Models.MessageModels
{
    public abstract class MessageModel
    {
        public string Message { get; set; } = string.Empty;
        public string MessageTitle { get; set; } = string.Empty;
        public string MessageColor { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
    }
}
