using Moq;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Common;

namespace MyApp.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private Mock<IPersonDataAccess> _mockDataAccess;
        private Mock<IMeetingDataAccess> _mockMeetingDataAccess;
        private UserManager _userManager;

        [TestInitialize]
        public void Initialize()
        {
            _mockDataAccess = new Mock<IPersonDataAccess>();
            _mockMeetingDataAccess = new Mock<IMeetingDataAccess>();
            _userManager = new UserManager(_mockDataAccess.Object, _mockMeetingDataAccess.Object);
        }

        [TestMethod]
        public void AddPerson_ShouldCallAddPerson_WhenUserIsValid()
        {
            var user = new Mentor("John", "Doe", "john@example.com", "Password123!", Role.Mentor);

            _userManager.AddPerson(user);

            _mockDataAccess.Verify(m => m.AddPerson(It.Is<User>(u => u.Email == "john@example.com")), Times.Once);
        }

        [TestMethod]
        public void RemovePerson_ShouldCallRemovePerson_WhenUserIsValid()
        {
            var user = new Mentor("John", "Doe", "john@example.com", "Password123!", Role.Mentor);

            _userManager.RemovePerson(user);

            _mockDataAccess.Verify(m => m.RemovePerson(It.Is<User>(u => u.Email == "john@example.com")), Times.Once);
        }

        [TestMethod]
        public void GetPersonByEmail_ShouldReturnPerson_WhenEmailIsValid()
        {
            var email = "john@example.com";
            var expectedPerson = new Mentor("John", "Doe", email, "password123", Role.Mentor);
            _mockDataAccess.Setup(m => m.GetPersonByEmail(email)).Returns(expectedPerson);

            var result = _userManager.GetPersonByEmail(email);

            Assert.AreEqual(expectedPerson, result);
        }

        [TestMethod]
        public void UpdatePersonInfo_WhenCalled_UpdatesPersonAndMeetingsIfNotAdmin()
        {
            var person = new Mentor("John", "Doe", "john@example.com", "Password123!", Role.Mentor, "imagePath");
            string newEmail = "newjohn@example.com";

            _mockDataAccess.Setup(m => m.UpdatePersonInfo(It.IsAny<IPerson>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Role>()))
                               .Returns(true);

            var result = _userManager.UpdatePersonInfo(person, "John", "Doe", newEmail, "newpassword", Role.Mentor);

            Assert.IsTrue(result);
            _mockMeetingDataAccess.Verify(m => m.UpdateMeetingEmails(person.Email, newEmail), Times.Once);
        }
    }
}