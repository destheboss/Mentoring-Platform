using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class UserManager
    {
        private readonly IPersonDataAccess data;

        public UserManager(IPersonDataAccess data)
        {
            this.data = data;
        }

        public void AddPerson(IPerson person)
        {
            data.AddPerson(person);
        }

        public void RemovePerson(IPerson person)
        {
            data.RemovePerson(person);
        }

        public void SuspendUser(User user)
        {
            data.SuspendUser(user);
        }

        public void UnsuspendUser(User user)
        {
            data.UnsuspendUser(user);
        }

        public void UpdateRating(Mentor mentor)
        {
            data.UpdateRating(mentor);
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return data.GetAdmins();
        }

        public IEnumerable<Mentor> GetMentors()
        {
            return data.GetMentors();
        }

        public IEnumerable<Mentee> GetMentees()
        {
            return data.GetMentees();
        }

        public bool[] CheckCredentialsForAdmin(string email, string password)
        {
            return data.CheckCredentialsForAdmin(email, password);
        }

        public bool[] CheckCredentialsForUser(string email, string password)
        {
            return data.CheckCredentialsForUser(email, password);
        }

        public IPerson? GetPersonByEmail(string email)
        {
            return data.GetPersonByEmail(email);
        }
    }
}