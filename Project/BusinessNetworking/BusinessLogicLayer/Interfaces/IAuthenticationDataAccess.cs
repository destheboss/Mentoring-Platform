using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAuthenticationDataAccess
    {
        (byte[] passwordHash, byte[] passwordSalt, string role) GetUserCredentials(string email);
    }
}