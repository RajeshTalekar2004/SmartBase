using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SmartBase.BusinessLayer.Services
{
    public class PasswordService
    {
        public string HashCreate(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                         password: value,
                         salt: Encoding.UTF8.GetBytes(salt),
                         prf: KeyDerivationPrf.HMACSHA512,
                         iterationCount: 10000,
                         numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public bool HashValidate(string value, string salt, string hash)
        {
            return HashCreate(value, salt) == hash;
        }

        public string SaltCreate()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
