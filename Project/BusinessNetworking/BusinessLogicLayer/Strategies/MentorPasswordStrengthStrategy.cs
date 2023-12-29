using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Strategies
{
    public class MentorPasswordStrengthStrategy : IPasswordStrengthStrategy
    {
        public bool IsPasswordStrong(string password)
        {
            bool hasUpper = false, hasLower = false;
            foreach (char c in password) // O(password.length)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
            }
            return hasUpper && hasLower && password.Length >= 8;
        }
    }
}