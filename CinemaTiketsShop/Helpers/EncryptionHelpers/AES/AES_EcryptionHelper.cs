using CinemaTiketsShop.Settings;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace CinemaTiketsShop.Helpers.EncryptionHelpers.AES
{
    public class AES_EcryptionHelper : IAES_EcryptionHelper
    {
        private readonly AES_Settings _aesSettings;

        private byte[] _aes => Convert.FromBase64String(_aesSettings.Key);
        private byte[] IV => Convert.FromBase64String(_aesSettings.IV);

        public AES_EcryptionHelper(IOptions<AES_Settings> options)
        {
            _aesSettings = options.Value;
        }

        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _aes;
                aes.IV = IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = _aes;
                aes.IV = IV;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(buffer))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
