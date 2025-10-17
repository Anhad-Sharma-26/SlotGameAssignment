using System.Security.Cryptography;

public static class SecureRandom
{
    // Returns a secure random integer between min (inclusive) and max (exclusive)
    public static int GetSecureRandomInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max);
    }
}
