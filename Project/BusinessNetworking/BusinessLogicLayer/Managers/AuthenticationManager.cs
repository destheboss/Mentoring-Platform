using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class AuthenticationManager
    {
        private readonly IAuthenticationDataAccess data;
        private readonly IHashingManager hashingManager;
        private readonly UserManager userManager;

        public AuthenticationManager(IAuthenticationDataAccess data, IHashingManager hashingManager, UserManager userManager)
        {
            this.data = data;
            this.hashingManager = hashingManager;
            this.userManager = userManager;
        }

        public bool[] CheckCredentialsForAdmin(string email, string password)
        {
            var (passwordHash, passwordSalt, role) = data.GetUserCredentials(email);
            if (passwordHash == null || passwordSalt == null)
            {
                return new bool[] { false, false };
            }

            bool isVerified = hashingManager.VerifyHash(password, passwordHash, passwordSalt);
            bool isAdmin = role == "Admin";
            return new bool[] { isAdmin, isVerified };
        }

        public bool CheckCredentialsForUser(string email, string password)
        {
            IPerson person = userManager.GetPersonByEmail(email);

            var (passwordHash, passwordSalt, _) = data.GetUserCredentials(email);
            if (passwordHash == null || passwordSalt == null)
            {
                return false;
            }

            if (person is User user)
            {
                if (user.isActive == false)
                {
                    return false;
                }
            }

            return hashingManager.VerifyHash(password, passwordHash, passwordSalt);
        }
    }
}