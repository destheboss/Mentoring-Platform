using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMeetingDataAccess
    {
        void AddMeeting(Meeting meeting);
        void CancelMeeting(Meeting meeting);
        void ChangeMeetingTime(Meeting meeting, DateTime newDate);
        void RateMeeting(Meeting meeting, int newRating);
        IEnumerable<Meeting> GetAllMeetings(string email);
        IEnumerable<Meeting> GetUpcomingMeetings(string email);
        IEnumerable<Meeting> GetPastMeetings(string email);
    }
}