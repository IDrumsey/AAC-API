using AnimalAdoptionCenterModels;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AnimalAdoptionCenter.Services.Authentication
{

    class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // https://stackoverflow.com/questions/2121913/is-there-a-built-in-function-to-hash-passwords-in-net
            var sha256Algorithm = new SHA256CryptoServiceProvider();
            byte[] hashedBytes = sha256Algorithm.ComputeHash(Encoding.Default.GetBytes(password));
            string hashed = Convert.ToBase64String(hashedBytes);

            return hashed;
        }
    }
}