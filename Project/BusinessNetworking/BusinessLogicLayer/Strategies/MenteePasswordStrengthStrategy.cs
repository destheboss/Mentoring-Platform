using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Strategies
{
    public class MenteePasswordStrengthStrategy : IPasswordStrengthStrategy
    {
        public bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return password.Length >= 8;
        }
    }
}