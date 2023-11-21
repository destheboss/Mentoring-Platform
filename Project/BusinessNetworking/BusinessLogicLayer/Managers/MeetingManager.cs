using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class MeetingManager
    {
        private readonly IMeetingDataAccess data;

        public MeetingManager(IMeetingDataAccess data)
        {
            this.data = data;
        }

        public void AddMeeting(Meeting meeting)
        {
            data.AddMeeting(meeting);
        }

        public void CancelMeeting(Meeting meeting)
        {
            data.CancelMeeting(meeting);
        }

        public void ChangeMeetingTime(Meeting meeting, DateTime newDate)
        {
            data.ChangeMeetingTime(meeting, newDate);
        }

        public virtual IEnumerable<Meeting> GetAllMeetings(string email)
        {
            return data.GetAllMeetings(email);
        }

        public IEnumerable<Meeting> GetUpcomingMeetings(string email)
        {
            return data.GetUpcomingMeetings(email);
        }

        public IEnumerable<Meeting> GetPastMeetings(string email)
        {
            return data.GetPastMeetings(email);
        }

        public virtual Meeting GetMeetingById(int meetingId)
        {
            return data.GetMeetingById(meetingId);
        }

        public virtual bool UpdateMeetingRating(Meeting? meeting)
        {
            return data.UpdateMeetingRating(meeting);
        }
    }
}