using BusinessLogicLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Mentee : User
    {
        // Creation of user
        public Mentee(string firstName, string lastName, string email, string password, Role role, string image = null)
            : base(firstName, lastName, email, password, role, image)
        {
        }

        // Pulling user from the database (excluding password for security reasons)
        public Mentee(int id, string firstName, string lastName, string email, Role role, bool isActive, string image = null)
        : base(id, firstName, lastName, email, role, isActive, image)
        {
        }
    }
}