using BusinessLogicLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Mentor : User
    {
        private float rating;

        public float Rating { get => this.rating; set => this.rating = value; }

        public Mentor(string name, string email, string password, Role role)
            : base(name, email, password, role)
        {
            this.Rating = 0;
        }

        public override string ToString() => $"{this.GetStatus} {this.Role} - rating: {this.Rating:f1}, name: {this.Name}, email: {this.Email}";
    }
}