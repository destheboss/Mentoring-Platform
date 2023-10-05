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
        public Mentee(string name, string email, string password, Role role)
            : base(name, email, password, role)
        {
        }
    }
}