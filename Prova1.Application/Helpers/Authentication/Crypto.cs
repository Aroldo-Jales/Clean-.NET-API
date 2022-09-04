using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Application.Helpers.Authentication
{
    public static class Crypto
    {
        public static byte[] GetSalt
        {
            get 
            {
                return RandomNumberGenerator.GetBytes(128 / 8);
            }
        }

        public static string ReturnUserHash(User user, string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: user.Email + password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256/8));

            return hashed;
        }
    }
}