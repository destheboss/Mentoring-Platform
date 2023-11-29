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
        private HashingManager _hashingManager;

        [TestInitialize]
        public void Initialize()
        {
            _mockDataAccess = new Mock<IPersonDataAccess>();
            _mockMeetingDataAccess = new Mock<IMeetingDataAccess>();
            _userManager = new UserManager(_mockDataAccess.Object, _mockMeetingDataAccess.Object, _hashingManager);
        }

        [TestMethod]
        public void AddPerson_ShouldCallAddPerson_WhenUserIsValid()
        {
            var user = new Mentee("John", "Doe", "john@example.com", "Password123!", Role.Mentee);

            _userManager.AddPerson(user);

            _mockDataAccess.Verify(m => m.AddPerson(It.Is<User>(u => u.Email == "john@example.com")), Times.Once);
        }

        [TestMethod]
        public void GetPersonByEmail_ShouldReturnPerson_WhenEmailIsValid()
        {
            var email = "john@example.com";
            var expectedPerson = new Mentee("John", "Doe", email, "password123", Role.Mentee);
            _mockDataAccess.Setup(m => m.GetPersonByEmail(email)).Returns(expectedPerson);

            var result = _userManager.GetPersonByEmail(email);

            Assert.AreEqual(expectedPerson, result);
        }
    }
}