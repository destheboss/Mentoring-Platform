using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class LoggingManager
    {
        private readonly IAuthenticationDataAccess data;

        public LoggingManager(IAuthenticationDataAccess data)
        {
            this.data = data;
        }

        public bool[] CheckCredentialsForAdmin(string email, string password)
        {
            return data.CheckCredentialsForAdmin(email, password);
        }

        public bool CheckCredentialsForUser(string email, string password)
        {
            return data.CheckCredentialsForUser(email, password);
        }
    }
}