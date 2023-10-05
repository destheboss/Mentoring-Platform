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
        private Role role;
        private bool _isActive;
        public List<Session> sessions;

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
        public bool isActive { get => this._isActive; set => this._isActive = value; }

        protected User(string name, string email, string password, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.isActive = true;
            this.sessions = new List<Session>();
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