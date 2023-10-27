using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class MeetingManager : IMeetingDataAccess
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

        public void RateMeeting(Meeting meeting, int rating)
        {
            data.RateMeeting(meeting, rating);
        }

        public IEnumerable<Meeting> GetAllMeetings(string email)
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
    }
}