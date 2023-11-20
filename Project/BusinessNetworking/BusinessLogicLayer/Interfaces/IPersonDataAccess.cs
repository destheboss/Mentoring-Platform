using BusinessLogicLayer.Common;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPersonDataAccess
    {
        void AddPerson(User person);
        void RemovePerson(User person);
        IEnumerable<Admin> GetAdmins();
        IEnumerable<Mentor> GetMentors();
        IEnumerable<Mentee> GetMentees();
        IPerson? GetPersonByEmail(string email);
        List<IPerson> GetAllPersons();
        bool UpdatePersonInfo(IPerson person, string newFirstName, string newLastName, string newEmail, string newPassword, Role newRole);
        void UpdateMentorAverageRating(Mentor mentor);
    }
}