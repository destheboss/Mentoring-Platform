using Moq;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Common;
using System.Security.Cryptography;

namespace MyApp.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private Mock<IPersonDataAccess> _personData;
        private Mock<IMeetingDataAccess> _meetingData;
        private Mock<IHashingManager> _hashingManager;
        private Mock<IPasswordStrengthStrategy> _passwordStrengthStrategy;
        private UserManager _userManager;

        [TestInitialize]
        public void Initialize()
        {
            _personData = new Mock<IPersonDataAccess>();
            _meetingData = new Mock<IMeetingDataAccess>();
            _hashingManager = new Mock<IHashingManager>();
            _passwordStrengthStrategy = new Mock<IPasswordStrengthStrategy>();
            _userManager = new UserManager(_personData.Object, _meetingData.Object, _hashingManager.Object);
        }

        [TestMethod]
        public void AddPerson_ShouldCallAddPerson_WhenUserIsValid()
        {
            var user = new Mentee("John", "Doe", "john@example.com", "Password123!", Role.Mentee);

            _hashingManager.Setup(h => h.GenerateHashWithSalt(It.IsAny<string>()))
                           .Returns(() => {
                               byte[] dummyHash = new byte[20];
                               byte[] dummySalt = new byte[16];
                               RandomNumberGenerator.Fill(dummyHash);
                               RandomNumberGenerator.Fill(dummySalt);
                               return (dummyHash, dummySalt);
                           });

            _userManager.AddPerson(user);

            _personData.Verify(d => d.AddPerson(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void AddPerson_ShouldThrowArgumentException_WhenPasswordIsInvalid()
        {
            var user = new Mentee("Jane", "Doe", "jane@example.com", "Pwd123", Role.Mentee);

            _hashingManager.Setup(h => h.GenerateHashWithSalt(It.IsAny<string>()))
                           .Returns(() => {
                               byte[] dummyHash = new byte[20];
                               byte[] dummySalt = new byte[16];
                               RandomNumberGenerator.Fill(dummyHash);
                               RandomNumberGenerator.Fill(dummySalt);
                               return (dummyHash, dummySalt);
                           });

            _passwordStrengthStrategy.Setup(p => p.IsPasswordStrong(It.IsAny<string>())).Returns(false);

            Assert.ThrowsException<ArgumentException>(() => _userManager.AddPerson(user));
        }

        [TestMethod]
        public void GetPersonByEmail_ShouldReturnPerson_WhenEmailIsValid()
        {
            var email = "john@example.com";
            var expectedPerson = new Mentee("John", "Doe", email, "password123", Role.Mentee);
            _personData.Setup(m => m.GetPersonByEmail(email)).Returns(expectedPerson);

            var result = _userManager.GetPersonByEmail(email);

            Assert.AreEqual(expectedPerson, result);
        }

        [TestMethod]
        public void RemovePerson_ShouldRemovePerson_WhenPersonExistsAndCanBeRemoved()
        {
            var user = new Mentee("John", "Doe", "john@example.com", "password123", Role.Mentee);

            _personData.Setup(d => d.RemovePerson(It.Is<User>(u => u.Email == user.Email)))
                       .Verifiable();

            _userManager.RemovePerson(user);

            _personData.Verify(d => d.RemovePerson(It.Is<User>(u => u.Email == user.Email)), Times.Once);
        }

        [TestMethod]
        public void RemovePerson_ShouldThrowException_WhenPersonHasDependentMeetings()
        {
            var user = new Mentee("John", "Doe", "john@example.com", "password123", Role.Mentee);

            _personData.Setup(d => d.RemovePerson(It.Is<User>(u => u.Email == user.Email)))
                       .Throws(new InvalidOperationException("Cannot delete person due to foreign key constraint."));

            Assert.ThrowsException<InvalidOperationException>(() => _userManager.RemovePerson(user));
        }

        [TestMethod]
        public void RemovePerson_ShouldThrowInvalidOperationException_WhenPersonDoesNotExist()
        {
            var user = new Mentee("NonExistent", "User", "nonexistent@example.com", "password123", Role.Mentee);

            _personData.Setup(d => d.RemovePerson(It.Is<User>(u => u.Email == user.Email)))
                       .Throws(new InvalidOperationException("Person not found in the database."));

            Assert.ThrowsException<InvalidOperationException>(() => _userManager.RemovePerson(user));
        }
    }
}