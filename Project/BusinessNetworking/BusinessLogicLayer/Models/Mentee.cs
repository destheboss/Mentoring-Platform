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
        public Mentee(string name, string email, string password, Role role)
            : base(name, email, password, role)
        {
        }

        // Pulling only necessary data for the user (excluding password for security reasons)
        public Mentee(string name, string email, Role role, bool isActive)
        : base(name, email, role, isActive)
        {
        }
    }
}