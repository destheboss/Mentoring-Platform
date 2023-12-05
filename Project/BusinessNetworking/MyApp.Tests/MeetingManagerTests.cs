using Moq;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Common;

namespace MyApp.Tests
{
    [TestClass]
    public class MeetingManagerTests
    {
        private Mock<IMeetingDataAccess> mockMeetingDataAccess;
        private MeetingManager meetingManager;

        [TestInitialize]
        public void Initialize()
        {
            mockMeetingDataAccess = new Mock<IMeetingDataAccess>();
            meetingManager = new MeetingManager(mockMeetingDataAccess.Object);
        }

        [TestMethod]
        public void AddMeeting_ValidMeeting_AddsMeeting()
        {
            var meetingManager = new MeetingManager(mockMeetingDataAccess.Object);
            var meeting = new Meeting(DateTime.Now.AddDays(1), 1, "mentor@example.com", 2, "mentee@example.com");

            meetingManager.AddMeeting(meeting);

            mockMeetingDataAccess.Verify(m => m.AddMeeting(It.IsAny<Meeting>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateMeetingRating_InvalidRating_DoesNotUpdate()
        {
            var meeting = new Meeting(DateTime.Now, 1, "mentor@example.com", 2, "mentee@example.com") { Rating = -1 };

            meetingManager.UpdateMeetingRating(meeting);
        }

        [TestMethod]
        public void UpdateMeetingRating_ValidRating_UpdatesSuccessfully()
        {
            var meeting = new Meeting(DateTime.Now, 1, "mentor@example.com", 2, "mentee@example.com");
            const int validRating = 5;
            meeting.Rating = validRating;

            mockMeetingDataAccess.Setup(m => m.UpdateMeetingRating(It.IsAny<Meeting>()))
                                 .Returns(true);

            var result = meetingManager.UpdateMeetingRating(meeting);

            Assert.IsTrue(result);
            mockMeetingDataAccess.Verify(m => m.UpdateMeetingRating(It.Is<Meeting>(mt => mt.Rating == validRating)), Times.Once);
        }
    }
}