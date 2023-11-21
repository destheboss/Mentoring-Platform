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

        [TestInitialize]
        public void Initialize()
        {
            mockMeetingDataAccess = new Mock<IMeetingDataAccess>();
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
        public void UpdateMeetingRating_InvalidRating_DoesNotUpdate()
        {
            var mockMeetingDataAccess = new Mock<IMeetingDataAccess>();
            var meetingManager = new MeetingManager(mockMeetingDataAccess.Object);

            var meeting = new Meeting(DateTime.Now, 1, "mentor@example.com", 2, "mentee@example.com") { Rating = -1 };

            var result = meetingManager.UpdateMeetingRating(meeting);

            Assert.IsFalse(result);
            mockMeetingDataAccess.Verify(m => m.UpdateMeetingRating(It.IsAny<Meeting>()), Times.Never);
        }

        [TestMethod]
        public void ChangeMeetingTime_ValidData_ChangesTime()
        {
            var mockMeetingDataAccess = new Mock<IMeetingDataAccess>();
            var meetingManager = new MeetingManager(mockMeetingDataAccess.Object);

            var meeting = new Meeting(DateTime.Now, 1, "mentor@example.com", 2, "mentee@example.com");
            var newDateTime = DateTime.Now.AddDays(2);

            meetingManager.ChangeMeetingTime(meeting, newDateTime);

            Assert.AreEqual(newDateTime, meeting.DateTime);
            mockMeetingDataAccess.Verify(m => m.ChangeMeetingTime(meeting, newDateTime), Times.Once);
        }
    }
}