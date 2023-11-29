using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Strategies
{
    public class AdminPasswordStrengthStrategy : IPasswordStrengthStrategy
    {
        public bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecialChar = false;
            int score = 0;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                else if (char.IsLower(c))
                    hasLower = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else
                    hasSpecialChar = true;
            }

            if (hasUpper) score++;
            if (hasLower) score++;
            if (hasDigit) score++;
            if (hasSpecialChar) score++;
            if (password.Length >= 8) score++;

            return score >= 4;
        }
    }
}