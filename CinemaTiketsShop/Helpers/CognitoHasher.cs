using CinemaTiketsShop.Configs;
using System.Security.Cryptography;
using System.Text;

namespace CinemaTiketsShop.Helpers
{
    public static class CognitoHasher
    {
        public static string HashUsername(string UserName, string AppClientId, string AppClientSecret)
        {
            string message = UserName + AppClientId;
            byte[] key = Encoding.UTF8.GetBytes(AppClientSecret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
