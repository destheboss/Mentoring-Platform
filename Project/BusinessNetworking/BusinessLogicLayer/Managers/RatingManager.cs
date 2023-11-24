using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class RatingManager
    {
        private readonly UserManager _userManager;
        private readonly MeetingManager _meetingManager;

        public RatingManager(UserManager userManager, MeetingManager meetingManager)
        {
            _userManager = userManager;
            _meetingManager = meetingManager;
        }

        public void UpdateMentorRating(string email)
        {
            var meetings = _meetingManager.GetAllMeetings(email);
            var totalRating = 0;
            var ratedMeetingsCount = 0;

            foreach (var meeting in meetings)
            {
                if (meeting.Rating > 0)
                {
                    totalRating += meeting.Rating;
                    ratedMeetingsCount++;
                }
            }

            if (ratedMeetingsCount > 0)
            {
                var averageRating = (float)totalRating / ratedMeetingsCount;
                Mentor mentor = _userManager.GetPersonByEmail(email) as Mentor;
                if (mentor != null)
                {
                    mentor.Rating = averageRating;
                    _userManager.UpdateMentorAverageRating(mentor);
                }
            }
        }

        public void RateMeeting(int meetingId, int rating)
        {
            var meeting = _meetingManager.GetMeetingById(meetingId);
            if (meeting != null)
            {
                meeting.Rating = rating;
                _meetingManager.UpdateMeetingRating(meeting);
                UpdateMentorRating(meeting.MentorEmail);
            }
        }

        public List<Mentor> GetTopRatedMentors()
        {
            var allMentors = _userManager.GetMentors().ToList();
            allMentors.Sort((x, y) => y.Rating.CompareTo(x.Rating));

            var topRatedMentors = new List<Mentor>();
            for (int i = 0; i < 10 && i < allMentors.Count; i++)
            {
                topRatedMentors.Add(allMentors[i]);
            }

            return topRatedMentors;
        }
    }
}