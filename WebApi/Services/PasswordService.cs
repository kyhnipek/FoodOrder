using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services;

public static class PasswordService
{
    private static byte[] GenerateSalt()
    {
        const int saltLength = 32;

        byte[] salt = new byte[saltLength];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
    public static string CreatePasswordHash(string password, out string salt)
    {

        byte[] saltBytes = GenerateSalt();
        salt = Convert.ToBase64String(saltBytes);

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, passwordBytes.Length, saltBytes.Length);

        var sha256 = SHA256.Create();

        byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
        return Convert.ToBase64String(hashBytes);
    }
    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        byte[] saltedPasswordBytes = new byte[passwordBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, passwordBytes.Length, saltBytes.Length);

        var sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
        string computedHash = Convert.ToBase64String(hashBytes);

        return computedHash.Equals(storedHash);
    }
}
