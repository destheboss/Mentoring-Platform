using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tests
{
    [TestClass]
    public class SuggestionAlgorithmTests
    {
        private Mock<IPersonDataAccess> _personData;
        private Mock<IMeetingDataAccess> _meetingData;
        private SuggestionManager suggestionManager;

        [TestInitialize]
        public void Initialize()
        {
            _personData = new Mock<IPersonDataAccess>();
            _meetingData = new Mock<IMeetingDataAccess>();
            suggestionManager = new SuggestionManager(_meetingData.Object, _personData.Object);

            _personData.Setup(m => m.GetMentorSpecialties(It.IsAny<int>()))
                       .Returns(new List<Specialty> { Specialty.WebDevelopment, Specialty.DataScience }); // Example specialties
        }

        [TestMethod]
        public void SuggestMentorsForMentee_ShouldReturnCorrectMentors()
        {
            var menteeEmail = "mentee@example.com";
            var mentors = new List<Mentor>
            {
                 new Mentor(1, "Alex", "Walker", "alex@example.com", Role.Mentor, true, 1, new List<Specialty> { Specialty.WebDevelopment }),
                 new Mentor(2, "Gordon", "Ramsay", "gordon@example.com", Role.Mentor, true, 5, new List<Specialty> { Specialty.Cooking }),
                 new Mentor(3, "Mark", "Trent", "mark@example.com", Role.Mentor, true, 5, new List<Specialty> { Specialty.DataScience }),
                 new Mentor(4, "John", "Doe", "john@example.com", Role.Mentor, true, 4, new List<Specialty> { Specialty.WebDevelopment }),
                 new Mentor(5, "Jane", "Doe", "jane@example.com", Role.Mentor, true, 2, new List<Specialty> { Specialty.DataScience }),
                 new Mentor(6, "Alice", "Smith", "alice@example.com", Role.Mentor, true, 3, new List<Specialty> { Specialty.SoftwareEngineering, Specialty.Cybersecurity }),
                 new Mentor(7, "Geaorge", "Knight", "george@example.com", Role.Mentor, true, 4, new List<Specialty> { Specialty.WebDevelopment }),
            };

            var meetings = new List<Meeting>
            {
                new Meeting(1, DateTime.Now.AddDays(-10), 1, "john@example.com", 101, menteeEmail, 5),
                new Meeting(2, DateTime.Now.AddDays(-20), 2, "jane@example.com", 101, menteeEmail, 3),
            };

            _personData.Setup(m => m.GetMentors()).Returns(mentors);
            _meetingData.Setup(m => m.GetAllMeetings(It.IsAny<string>())).Returns(meetings);

            var suggestedMentors = suggestionManager.SuggestMentorsForMentee(menteeEmail).ToList();

            Assert.IsTrue(suggestedMentors.Count > 0);
            Assert.IsTrue(suggestedMentors.Any(m => m.Email == "john@example.com"));
        }

        [TestMethod]
        public void SuggestMentorsForMentee_ShouldNotSuggestMentorsWithNoMatchingSpecialties()
        {
            var menteeEmail = "mentee@example.com";

            var mentors = new List<Mentor>
            {
                new Mentor(1, "Gordon", "Ramsay", "gordon@example.com", Role.Mentor, true, 5, new List<Specialty> { Specialty.Cooking }),
                new Mentor(1, "Alberta", "Jensen", "alberta@example.com", Role.Mentor, true, 5, new List<Specialty> { Specialty.CareerCoaching }),
            };

            var meetings = new List<Meeting>
            {
                new Meeting(1, DateTime.Now.AddDays(-10), 1, "alberta@example.com", 101, menteeEmail, 5),
            };

            _personData.Setup(m => m.GetMentors()).Returns(mentors);
            _meetingData.Setup(m => m.GetAllMeetings(It.IsAny<string>())).Returns(meetings);

            var suggestedMentors = suggestionManager.SuggestMentorsForMentee(menteeEmail).ToList();

            Assert.IsFalse(suggestedMentors.Any(m => m.Email == "gordon@example.com"));
        }

        [TestMethod]
        public void SuggestMentorsForMentee_ShouldHandleMenteeWithNoPriorMeetings()
        {
            var menteeEmail = "newmentee@example.com";
            var mentors = new List<Mentor>
            {
                new Mentor(1, "Gordon", "Ramsay", "gordon@example.com", Role.Mentor, true, 5, new List<Specialty> { Specialty.Cooking }),
            };

            var meetings = new List<Meeting>(); // Empty list indicating no prior meetings

            _personData.Setup(m => m.GetMentors()).Returns(mentors);
            _meetingData.Setup(m => m.GetAllMeetings(It.IsAny<string>())).Returns(meetings);

            var suggestedMentors = suggestionManager.SuggestMentorsForMentee(menteeEmail).ToList();

            Assert.IsTrue(suggestedMentors.Count > 0);
        }
    }
}