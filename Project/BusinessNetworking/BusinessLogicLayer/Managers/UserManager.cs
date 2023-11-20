using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicLayer.Managers
{
    public class UserManager
    {
        private readonly IPersonDataAccess data;
        private readonly IMeetingDataAccess meetingData;

        public UserManager(IPersonDataAccess data, IMeetingDataAccess meetingData)
        {
            this.data = data;
            this.meetingData = meetingData;
        }

        public void AddPerson(User user)
        {
            data.AddPerson(user);
        }

        public void RemovePerson(User user)
        {
            data.RemovePerson(user);
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

        public IPerson? GetPersonByEmail(string email)
        {
            return data.GetPersonByEmail(email);
        }

        public List<IPerson> GetAllPersons()
        {
            return data.GetAllPersons();
        }

        public bool UpdatePersonInfo(IPerson person, string newFirstName, string newLastName, string newEmail, string newPassword, Role newRole)
        {
            using (var transactionScope = new TransactionScope())
            {
                var oldEmail = person.Email;

                bool personUpdated = data.UpdatePersonInfo(person, newFirstName, newLastName, newEmail, newPassword, newRole);
                if (!personUpdated)
                {
                    return false;
                }

                if (!person.Role.Equals("Admin"))
                {
                    meetingData.UpdateMeetingEmails(oldEmail, newEmail);
                }

                transactionScope.Complete();
                return true;
            }
        }

        public void UpdateMentorAverageRating(Mentor mentor)
        {
            data.UpdateMentorAverageRating(mentor);
        }
    }
}