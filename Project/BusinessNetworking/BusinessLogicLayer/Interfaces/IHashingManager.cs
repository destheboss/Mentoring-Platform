using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHashingManager
    {
        (byte[] hash, byte[] salt) GenerateHashWithSalt(string password);
        byte[] GenerateHash(string password, byte[] saltBytes);
        bool VerifyHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}