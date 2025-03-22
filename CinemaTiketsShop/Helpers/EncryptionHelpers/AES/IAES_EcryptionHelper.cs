namespace CinemaTiketsShop.Helpers.EncryptionHelpers.AES
{
    public interface IAES_EcryptionHelper
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
