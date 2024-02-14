namespace EncodingService.EncodingService;

public interface IEncoding
{
    byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes);
    byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes);
    Task<string> Encrypt(string text);
    Task<string> Decrypt(string decryptedText);
    bool TryToDecrypt(string decryptedText);
    byte[] GetBytesToDecode(string text);
    byte[] GetRandomBytes();
    string EncryptionNormalizedEmail(string decryptedText);
    string DecryptionNormalizedEmail(string encriptedText);
}