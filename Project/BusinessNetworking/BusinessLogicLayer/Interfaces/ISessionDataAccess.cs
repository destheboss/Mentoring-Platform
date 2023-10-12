using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISessionDataAccess
    {
        void AddSession(Session session);
        void CancelSession(Session session);
        void EditSession(Session session, DateTime newDate, int newRating);
        void RateSession(Session session, int newRating);
        IEnumerable<Session> GetAllSessions(string email);
        IEnumerable<Session> GetUpcomingSessions(string email);
        IEnumerable<Session> GetPastSessions(string email);
    }
}
