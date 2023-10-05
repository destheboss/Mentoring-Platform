using BusinessLogicLayer.Common;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class SessionManager
    {
        private UserManager _userManager;

        public SessionManager(UserManager userManager)
        {
            _userManager = userManager;
        }

        public void AddSession(Session session)
        {
            foreach (User user in _userManager.Users)
            {
                if (user is Mentor && user.Email == session.MentorEmail)
                {
                    ((Mentor)user).sessions.Add(session);
                }
                if (user is Mentee && user.Email == session.MenteeEmail)
                {
                    ((Mentee)user).sessions.Add(session);
                }
            }
        }

        public void CancelSession(Session session)
        {
            foreach (User user in _userManager.Users)
            {
                if (user is Mentor && user.Email == session.MentorEmail)
                {
                    ((Mentor)user).sessions.Remove(session);
                }
                if (user is Mentee && user.Email == session.MenteeEmail)
                {
                    ((Mentee)user).sessions.Remove(session);
                }
            }
        }

        public void EditSession(Session session, DateTime newDate, int newRating)
        {
            bool mentorFound = false, menteeFound = false;

            foreach (User user in _userManager.Users)
            {
                if (!mentorFound && user is Mentor && user.Email == session.MentorEmail)
                {
                    foreach (Session foundSession in ((Mentor)user).sessions)
                    {
                        if (session.DateTime == foundSession.DateTime &&
                            session.MenteeEmail == foundSession.MenteeEmail)
                        {
                            foundSession.DateTime = newDate;
                            foundSession.Rating = newRating;
                            mentorFound = true;
                            break;
                        }
                    }
                }

                if (!menteeFound && user is Mentee && user.Email == session.MenteeEmail)
                {
                    foreach (Session foundSession in ((Mentee)user).sessions)
                    {
                        if (session.DateTime == foundSession.DateTime &&
                            session.MentorEmail == foundSession.MentorEmail)
                        {
                            foundSession.DateTime = newDate;
                            foundSession.Rating = newRating;
                            menteeFound = true;
                            break;
                        }
                    }
                }

                if (mentorFound && menteeFound) break;
            }
        }
        
        public void RateSession(Session session, int rating)
        {
            session.Rating = rating;
        }

        public List<Session> GetSessions(string userEmail)
        {
            List<Session> sessions = new List<Session>();

            foreach (User user in _userManager.Users)
            {
                if (user.Email == userEmail)
                {
                    foreach (var session in user.sessions)
                    {
                        sessions.Add(session);
                    }
                }
            }
            return sessions;
        }
    }
}
