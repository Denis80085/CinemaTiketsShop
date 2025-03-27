using CinemaTiketsShop.Models.MessageModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CinemaTiketsShop.Helpers.MessageProviderHellper
{
    public static class MessageHellper
    {
        public static void SendFailureMessageToView(this Controller controller, string messageText) 
        {
            controller.ViewBag.Message = new FailureMessage(messageText);
        }

        public static void SendSuccessMessageToView(this Controller controller, string messageText)
        {
            controller.ViewBag.Message = new SuccessMessage(messageText);
        }

        public static void SendWarningMessageToView(this Controller controller, string messageText)
        {
            controller.ViewBag.Message = new WarningMessage(messageText);
        }

        public static void SendFailureMessageToAction(this Controller controller, string messageText)
        {
            var FailureMessage = new FailureMessage(messageText);

            var serializedMessage = JsonConvert.SerializeObject(messageText);

            controller.TempData["Message"] = serializedMessage;
        }

        public static void SendSuccessMessageToAction(this Controller controller, string messageText)
        {
            var SuccessMessage = new SuccessMessage(messageText);

            var serializedMessage = JsonConvert.SerializeObject(SuccessMessage);

            controller.TempData["Message"] = serializedMessage;
        }

        public static void SendWarningMessageToAction(this Controller controller, string messageText)
        {
            var WarningMessage = new WarningMessage(messageText);

            var serializedMessage = JsonConvert.SerializeObject(WarningMessage);

            controller.TempData["Message"] = serializedMessage;
        }
    }
}
