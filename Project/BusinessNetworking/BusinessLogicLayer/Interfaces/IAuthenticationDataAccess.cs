using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAuthenticationDataAccess
    {
        bool[] CheckCredentialsForAdmin(string email, string password);
        bool CheckCredentialsForUser(string email, string password);
    }
}