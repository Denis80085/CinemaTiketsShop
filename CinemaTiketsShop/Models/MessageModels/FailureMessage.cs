namespace CinemaTiketsShop.Models.MessageModels
{
    public class FailureMessage : MessageModel
    {
        public FailureMessage(string message)
        {
            Message = message;
            MessageTitle = "Failure";
            MessageColor = "danger";
            StatusCode = System.Net.HttpStatusCode.Forbidden;
        }
    }
}
