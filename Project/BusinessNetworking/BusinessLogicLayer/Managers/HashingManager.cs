using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class HashingManager
    {
        public (byte[] hash, byte[] salt) GenerateHashWithSalt(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            byte[] hashBytes = GenerateHash(password, saltBytes);

            return (hashBytes, saltBytes);
        }

        public byte[] GenerateHash(string password, byte[] saltBytes)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 100000))
            {
                return pbkdf2.GetBytes(20);
            }
        }

        public bool VerifyHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            byte[] newHash = GenerateHash(password, storedSalt);
            return StructuralComparisons.StructuralEqualityComparer.Equals(newHash, storedHash);
        }
    }
}