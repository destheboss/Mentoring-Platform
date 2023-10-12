﻿using BusinessLogicLayer.Interfaces;
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
        private string passwordHash;
        private string passwordSalt;
        private Role role;
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

        // Creation of user
        public Admin(string name, string email, string password, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }

        // Pulling only necessary data for the user (excluding password for security reasons)
        public Admin(string name, string email, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Role = role;
        }

        public override string ToString() => $"{this.Role} - name: {this.Name}, email: {this.Email}";
    }
}