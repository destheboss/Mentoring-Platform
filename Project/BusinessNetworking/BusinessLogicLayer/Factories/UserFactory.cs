using BusinessLogicLayer.Common;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Factories
{
    public static class UserFactory
    {
        public static User CreateUser(string firstName, string lastName, string email, string password, Role role, List<Specialty> specialties = null, string image = null)
        {
            switch (role)
            {
                case Role.Mentor:
                    return new Mentor(firstName, lastName, email, password, role, specialties ?? new List<Specialty>(), image);
                case Role.Mentee:
                    return new Mentee(firstName, lastName, email, password, role, image);
                default:
                    throw new ArgumentException("Invalid role");
            }
        }
    }
}