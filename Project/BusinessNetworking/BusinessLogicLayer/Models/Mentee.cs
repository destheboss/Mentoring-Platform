using BusinessLogicLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Mentee : User
    {
        // Creation of user
        public Mentee(string firstName, string lastName, string email, string password, Role role)
            : base(firstName, lastName, email, password, role)
        {
        }

        // Pulling only necessary data for the user (excluding password for security reasons)
        public Mentee(string firstName, string lastName, string email, Role role, bool isActive)
        : base(firstName, lastName, email, role, isActive)
        {
        }
    }
}