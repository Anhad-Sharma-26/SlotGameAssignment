using System.Security.Cryptography;

public static class SecureRandom
{
    // Returns a secure random integer between min  and max 
    public static int GetSecureRandomInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max);
    }
}
