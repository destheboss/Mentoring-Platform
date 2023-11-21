using Moq;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Common;

namespace MyApp.Tests
{
    [TestClass]
    public class RatingManagerTests
    {
        private Mock<IPersonDataAccess> _mockPersonDataAccess;
        private Mock<IMeetingDataAccess> _mockMeetingDataAccess;
        private Mock<UserManager> _mockUserManager;
        private Mock<MeetingManager> _mockMeetingManager;
        private RatingManager _ratingManager;

        [TestInitialize]
        public void Initialize()
        {
            _mockPersonDataAccess = new Mock<IPersonDataAccess>();
            _mockMeetingDataAccess = new Mock<IMeetingDataAccess>();

            _mockUserManager = new Mock<UserManager>(_mockPersonDataAccess.Object, _mockMeetingDataAccess.Object);
            _mockMeetingManager = new Mock<MeetingManager>(_mockMeetingDataAccess.Object);
            _ratingManager = new RatingManager(_mockUserManager.Object, _mockMeetingManager.Object);
        }

        [TestMethod]
        public void RateMeeting_ValidMeeting_SuccessfulRatingUpdate()
        {
            int meetingId = 1;
            int newRating = 5;
            var meeting = new Meeting(DateTime.Now, 1, "mentor@example.com", 2, "mentee@example.com") { Id = meetingId };

            _mockMeetingManager.Setup(m => m.GetMeetingById(meetingId)).Returns(meeting);

            _ratingManager.RateMeeting(meetingId, newRating);

            Assert.AreEqual(newRating, meeting.Rating);
            _mockMeetingManager.Verify(m => m.UpdateMeetingRating(meeting), Times.Once);
        }

        [TestMethod]
        public void RateMeeting_InvalidMeeting_NoRatingUpdate()
        {
            int meetingId = 1;
            int newRating = 5;

            _mockMeetingManager.Setup(m => m.GetMeetingById(meetingId)).Returns((Meeting)null);

            _ratingManager.RateMeeting(meetingId, newRating);

            _mockMeetingManager.Verify(m => m.UpdateMeetingRating(It.IsAny<Meeting>()), Times.Never);
        }

        [TestMethod]
        public void UpdateMentorRating_ValidEmail_SuccessfulRatingUpdate()
        {
            string email = "mentor@example.com";
            var meetings = new List<Meeting>
            {
                new Meeting(1, DateTime.Now, 1, email, 2, "mentee@example.com", 4),
                new Meeting(2, DateTime.Now.AddDays(1), 1, email, 3, "mentee2@example.com", 5)
            };

            _mockMeetingManager.Setup(m => m.GetAllMeetings(email)).Returns(meetings);
            var mentor = new Mentor("John", "Doe", email, "password", Role.Mentor, "image/path");

            _mockUserManager.Setup(m => m.GetPersonByEmail(email)).Returns(mentor);

            _ratingManager.UpdateMentorRating(email);

            Assert.AreEqual(4.5f, mentor.Rating);
            _mockUserManager.Verify(m => m.UpdateMentorAverageRating(mentor), Times.Once);
        }

        [TestMethod]
        public void UpdateMentorRating_NoMeetings_NoRatingUpdate()
        {
            string email = "mentor@example.com";
            var meetings = new List<Meeting>();

            _mockMeetingManager.Setup(m => m.GetAllMeetings(email)).Returns(meetings);
            var mentor = new Mentor("John", "Doe", email, "password", Role.Mentor, "image/path");

            _mockUserManager.Setup(m => m.GetPersonByEmail(email)).Returns(mentor);

            _ratingManager.UpdateMentorRating(email);

            Assert.AreEqual(0, mentor.Rating);
            _mockUserManager.Verify(m => m.UpdateMentorAverageRating(It.IsAny<Mentor>()), Times.Never);
        }
    }
}