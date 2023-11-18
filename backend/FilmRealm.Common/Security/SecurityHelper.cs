using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FilmRealm.Common.Security;

public static class SecurityHelper
{
    private const int DefBytesLength = 32;
    
    public static string HashPassword(string password, string salt) 
        => Convert.ToBase64String(
        KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        )
    );

    public static string GenerateSalt()
    {
        using var randomNumber = RandomNumberGenerator.Create();
        var salt = new byte[DefBytesLength];
        randomNumber.GetBytes(salt);

        return Convert.ToBase64String(salt);
    }

    public static bool PasswordValidation(string password, string hash, string salt)
        => hash == HashPassword(password, salt);
}