using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class UserManager
    {
        public List<IPerson> Users;

        public UserManager()
        {
            this.Users = new List<IPerson>();
        }

        public void AddPerson(IPerson person)
        {
            this.Users.Add(person);
        }

        public void RemovePerson(IPerson person)
        {
            this.Users.Remove(person);
        }

        public void SuspendUser(User user)
        {
            user.isActive = false;
        }

        public void UnsuspendUser(User user)
        {
            user.isActive = true;
        }

        public float CalculateAverageRating(Mentor mentor)
        {
            if (mentor.sessions.Count == 0)
            {
                return 0;
            }

            float score = 0;

            foreach (var session in mentor.sessions)
            {
                score += session.Rating;
            }
            return score / mentor.sessions.Count;
        }

        public void UpdateRating(Mentor mentor)
        {
            mentor.Rating = CalculateAverageRating(mentor);
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();

            foreach (IPerson person in this.Users)
            {
                if (person is Admin admin)
                {
                    admins.Add(admin);
                }
            }
            return admins;
        }

        public List<Mentor> GetMentors()
        {
            List<Mentor> mentors = new List<Mentor>();

            foreach (IPerson person in this.Users)
            {
                if (person is Mentor mentor)
                {
                    mentors.Add(mentor);
                }
            }
            return mentors;
        }

        public List<Mentee> GetMentees()
        {
            List<Mentee> mentees = new List<Mentee>();

            foreach (IPerson person in this.Users)
            {
                if (person is Mentee mentee)
                {
                    mentees.Add(mentee);
                }
            }
            return mentees;
        }

        public bool[] CheckCredentialsForAdmin(string email, string password)
        {
            bool[] results = new bool[2] { false, false };

            foreach (var user in this.Users)
            {
                if (user.Email == email && user.Password == password)
                {
                    results[0] = true;

                    if (user.Role == Role.Admin)
                    {
                        results[1] = true;
                        return results;
                    }

                    return results;
                }
            }
            return results;
        }

        public bool CheckCredentialsForUser(string email, string password)
        {
            foreach (var user in this.Users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}