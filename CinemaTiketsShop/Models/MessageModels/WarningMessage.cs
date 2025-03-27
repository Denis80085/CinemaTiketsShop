namespace CinemaTiketsShop.Models.MessageModels
{
    public class WarningMessage : MessageModel
    {
        public WarningMessage(string message)
        {
            Message = message;
            MessageTitle = "Warning";
            MessageColor = "warning";
            StatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}
