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

        public List<Specialty> Specialties { get; set; }

        // Creation of user
        public Mentor(string firstName, string lastName, string email, string password, Role role, List<Specialty> specialties, string image = null)
            : base(firstName, lastName, email, password, role, image)
        {
            Specialties = specialties ?? new List<Specialty>();
        }

        // Pulling user from the database (excluding password for security reasons)
        public Mentor(int id, string firstName, string lastName, string email, Role role, bool isActive, float rating, List<Specialty> specialties, string image = null)
        : base(id, firstName, lastName, email, role, isActive, image)
        {
            this.Rating = rating;
            this.Specialties = specialties ?? new List<Specialty>();
        }

        public override string ToString() => $"{this.FirstName} {this.LastName} - ({GetStatus()}) {this.Role} - email: {this.Email}, Rating: {this.Rating:f1}";
    }
}