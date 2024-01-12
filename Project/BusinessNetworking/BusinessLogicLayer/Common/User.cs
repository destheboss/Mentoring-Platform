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
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string passwordHash;
        private string passwordSalt;
        private Role role;
        private bool _isActive;
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
        public bool isActive { get => this._isActive; set => this._isActive = value; }
        public string Image { get; set; }

        // Creation of user
        protected User(string firstName, string lastName, string email, string password, Role role, string image = null)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.isActive = true;
            this.Image = image;
        }

        // Pulling user from the database (excluding password for security reasons)
        protected User(int id, string firstName, string lastName, string email, Role role, bool isActive, string image = null)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Role = role;
            this.isActive = isActive;
            this.Image = image;
        }

        public string GetStatus()
        {
            return this.isActive ? "Active" : "Suspended";
        }

        public override string ToString() => $"{this.FirstName} {this.LastName} - ({GetStatus()}) {this.Role} - email: {this.Email}";
    }
}