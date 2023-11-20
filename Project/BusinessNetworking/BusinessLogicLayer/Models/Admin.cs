using BusinessLogicLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Admin : User
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string passwordHash;
        private string passwordSalt;
        private Role role;
        private string image;

        public int Id
        {
            get => this.id;
            private set => this.id = value;
        }
        public string FirstName
        {
            get => this.firstName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid name.");
                }
                this.firstName = value;
            }
        }
        public string LastName
        {
            get => this.lastName;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid name.");
                }
                this.lastName = value;
            }
        }
        public string Email
        {
            get => this.email;
            private set
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
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid password.");
                }
                this.password = value;
            }
        }
        public string PasswordHash { get => this.passwordHash; set => this.passwordHash = value; }
        public string PasswordSalt { get => this.passwordSalt; set => this.passwordSalt = value; }
        public Role Role
        {
            get => this.role;
            private set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Role cannot be empty.");
                }
                this.role = value;
            }
        }
        public string Image { get; set; }

        // Creation of user
        public Admin(string firstName, string lastName, string email, string password, Role role, string image = null)
            : base(firstName, lastName, email, password, role, image)
        {
        }

        // Pulling user from the database (excluding password for security reasons)
        public Admin(int id, string firstName, string lastName, string email, Role role, bool isActive, string image = null)
        : base(id, firstName, lastName, email, role, isActive, image)
        {
        }
    }
}