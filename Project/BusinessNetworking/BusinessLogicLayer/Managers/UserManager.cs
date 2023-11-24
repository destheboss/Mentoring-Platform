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
        private readonly HashingManager hashingManager;
        private readonly PasswordStrengthChecker passwordStrengthChecker;

        public UserManager(IPersonDataAccess data, IMeetingDataAccess meetingData, HashingManager hashingManager, PasswordStrengthChecker passwordStrengthChecker)
        {
            this.data = data;
            this.meetingData = meetingData;
            this.hashingManager = hashingManager;
            this.passwordStrengthChecker = passwordStrengthChecker;
        }

        public void AddPerson(User user)
        {
            if (!passwordStrengthChecker.IsPasswordStrong(user.Password))
            {
                throw new ArgumentException("Password does not meet the strength requirements.");
            }

            var (hash, salt) = hashingManager.GenerateHashWithSalt(user.Password);
            user.PasswordHash = Convert.ToBase64String(hash);
            user.PasswordSalt = Convert.ToBase64String(salt);

            data.AddPerson(user);
        }

        public void RemovePerson(User user)
        {
            data.RemovePerson(user);
            if (!string.IsNullOrWhiteSpace(user.Image))
            {
                string imagePath = Path.Combine("wwwroot", user.Image);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
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

        public virtual IPerson? GetPersonByEmail(string email)
        {
            return data.GetPersonByEmail(email);
        }

        public List<IPerson> GetAllPersons()
        {
            return data.GetAllPersons();
        }

        public bool UpdatePersonInfo(User currentUser, User updatedUser)
        {
            using (var transactionScope = new TransactionScope())
            {
                var oldEmail = currentUser.Email;

                byte[] hash, salt;
                if (!string.IsNullOrEmpty(updatedUser.Password))
                {
                    (hash, salt) = hashingManager.GenerateHashWithSalt(updatedUser.Password);
                    updatedUser.PasswordHash = Convert.ToBase64String(hash);
                    updatedUser.PasswordSalt = Convert.ToBase64String(salt);
                }
                else
                {
                    updatedUser.PasswordHash = currentUser.PasswordHash;
                    updatedUser.PasswordSalt = currentUser.PasswordSalt;
                }

                bool personUpdated = data.UpdatePersonInfo(currentUser, updatedUser);
                if (!personUpdated)
                {
                    return false;
                }

                if (!currentUser.Role.Equals("Admin"))
                {
                    meetingData.UpdateMeetingEmails(oldEmail, updatedUser.Email);
                }

                transactionScope.Complete();
                return true;
            }
        }

        public virtual void UpdateMentorAverageRating(Mentor mentor)
        {
            data.UpdateMentorAverageRating(mentor);
        }
    }
}