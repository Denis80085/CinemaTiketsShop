using Microsoft.Identity.Client;
using System.Net;

namespace CinemaTiketsShop.Helpers
{
    public static class PictureUrl
    {
        private readonly static List<string> AllowedTypes = ["jpg", "png", "svg", "webp"];
        
        public static bool isValid(string? Uri) 
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
            request.Method = "HEAD";

            try
            {
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK && AllowedTypes.Contains(response.ContentType.Trim().ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
