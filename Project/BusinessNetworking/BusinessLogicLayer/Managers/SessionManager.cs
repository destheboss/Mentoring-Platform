using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class SessionManager : ISessionDataAccess
    {
        private readonly ISessionDataAccess data;

        public SessionManager(ISessionDataAccess data)
        {
            this.data = data;
        }

        public void AddSession(Session session)
        {
            data.AddSession(session);
        }

        public void CancelSession(Session session)
        {
            data.CancelSession(session);
        }

        public void EditSession(Session session, DateTime newDate, int newRating)
        {
            data.EditSession(session, newDate, newRating);
        }

        public void RateSession(Session session, int rating)
        {
            data.RateSession(session, rating);
        }

        public IEnumerable<Session> GetAllSessions(string email)
        {
            return data.GetAllSessions(email);
        }

        public IEnumerable<Session> GetUpcomingSessions(string email)
        {
            return data.GetUpcomingSessions(email);
        }

        public IEnumerable<Session> GetPastSessions(string email)
        {
            return data.GetPastSessions(email);
        }
    }
}