using System.Security.Cryptography;

public static class PasswordHasher
{
    public static (string Hash, string Salt) HashPassword(string password, int iterations = 100_000)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public static bool Verify(string password, string base64Hash, string base64Salt, int iterations = 100_000)
    {
        var salt = Convert.FromBase64String(base64Salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(32);
        var stored = Convert.FromBase64String(base64Hash);
        return CryptographicOperations.FixedTimeEquals(hash, stored);
    }
}
