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
    }
}