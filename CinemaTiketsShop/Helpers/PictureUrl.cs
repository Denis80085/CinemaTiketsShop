using Microsoft.Identity.Client;
using System.Net;
using System.Runtime.CompilerServices;

namespace CinemaTiketsShop.Helpers
{
    public static class PictureUrl
    {
        private readonly static List<string?> AllowedTypes = ["image/jpeg", "image/png", "image/svg", "image/webp"];
        
        public async static Task<bool> isValid(string? Uri) 
        {
            using HttpClient client = new();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Head, Uri);

                var response = await client.SendAsync(request);

                if(response == null) 
                {
                    return false;
                }

                if (response.StatusCode == HttpStatusCode.OK && AllowedTypes.Contains(response.Content.Headers.ContentType?.MediaType?.Trim().ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch (Exception) 
            {
                return false;
            }
        }
    }
}
