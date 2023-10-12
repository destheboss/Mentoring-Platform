using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Common
{
    public abstract class User : IPerson
    {
        private string name;
        private string email;
        private string password;
        private string passwordHash;
        private string passwordSalt;
        private Role role;
        private bool _isActive;

        public string Name 
        { 
            get => this.name;          
            private set
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
        public Role Role { get => this.role; private set => this.role = value; }
        public bool isActive { get => this._isActive; set => this._isActive = value; }

        // Creation of user
        protected User(string name, string email, string password, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.isActive = true;
        }

        // Pulling only necessary data for the user (excluding password for security reasons)
        protected User(string name, string email, Role role, bool isActive)
        {
            this.Name = name;
            this.Email = email;
            this.Role = role;
            this.isActive = isActive;
        }

        public string GetStatus(User user)
        {
            if (user.isActive == true)
            {
                return "Active";
            }
            return "Suspended";
        }

        public override string ToString() => $"{this.GetStatus} {this.Role} - name: {this.Name}, email: {this.Email}";
    }
}