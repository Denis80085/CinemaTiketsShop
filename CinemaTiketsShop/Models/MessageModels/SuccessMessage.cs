namespace CinemaTiketsShop.Models.MessageModels
{
    public class SuccessMessage : MessageModel
    {
        public SuccessMessage(string message)
        {
            Message = message;
            MessageTitle = "Success";
            MessageColor = "success";
            StatusCode = System.Net.HttpStatusCode.OK;
        }
    }
}
