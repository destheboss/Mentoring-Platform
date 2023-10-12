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
        void AddPerson(IPerson person);
        void RemovePerson(IPerson person);
        void SuspendUser(User user);
        void UnsuspendUser(User user);
        void UpdateRating(Mentor mentor);
        IEnumerable<Admin> GetAdmins();
        IEnumerable<Mentor> GetMentors();
        IEnumerable<Mentee> GetMentees();
        bool[] CheckCredentialsForAdmin(string email, string password);
        bool[] CheckCredentialsForUser(string email, string password);
        IPerson? GetPersonByEmail(string email);
    }
}
