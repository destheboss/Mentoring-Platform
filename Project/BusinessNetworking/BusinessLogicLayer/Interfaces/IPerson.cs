using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPerson
    {
        string Name { get; }
        string Email { get; }
        string Password { get; }
        string PasswordHash { get; set; }
        string PasswordSalt { get; set; }
        Role Role { get; }
    }
}
