using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Admin : IPerson
    {
        private string name;
        private string email;
        private string password;
        private Role role;

        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid name.");
                }
                this.name = value;
            }
        }
        public string Email
        {
            get => this.email;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid email address.");
                }
                this.email = value;
            }
        }
        public string Password
        {
            get => this.password;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid password.");
                }
                this.password = value;
            }
        }
        public Role Role { get => this.role; set => this.role = value; }

        public Admin(string name, string email, string password, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }

        public override string ToString() => $"{this.Role} - name: {this.Name}, email: {this.Email}";
    }
}