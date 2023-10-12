using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;

namespace DataAccessLayer.Managers
{
    public class SessionDataManager : ConnectionSQL, ISessionDataAccess
    {
        public void AddSession(Session session)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO Session (DateTime, MentorEmail, MenteeEmail, Rating) VALUES (@DateTime, @MentorEmail, @MenteeEmail, @Rating)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DateTime", session.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", session.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", session.MenteeEmail);
                    cmd.Parameters.AddWithValue("@Rating", session.Rating);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CancelSession(Session session)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Session WHERE DateTime = @DateTime AND MentorEmail = @MentorEmail AND MenteeEmail = @MenteeEmail AND Rating = @Rating";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DateTime", session.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", session.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", session.MenteeEmail);
                    cmd.Parameters.AddWithValue("@Rating", session.Rating);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Session not found in the database.");
                    }
                }
            }
        }

        public void EditSession(Session session, DateTime newDate, int newRating)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Session SET DateTime = @NewDateTime, Rating = @NewRating WHERE DateTime = @OldDateTime AND MentorEmail = @MentorEmail AND MenteeEmail = @MenteeEmail";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewDateTime", newDate);
                    cmd.Parameters.AddWithValue("@NewRating", newRating);

                    cmd.Parameters.AddWithValue("@OldDateTime", session.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", session.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", session.MenteeEmail);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Session not found in the database.");
                    }
                }
            }
        }

        public void RateSession(Session session, int newRating)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Session SET Rating = @NewRating WHERE DateTime = @DateTime AND MentorEmail = @MentorEmail AND MenteeEmail = @MenteeEmail";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewRating", newRating);

                    cmd.Parameters.AddWithValue("@DateTime", session.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", session.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", session.MenteeEmail);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Session not found in the database.");
                    }
                }
            }
        }

        private IEnumerable<Session> GetSessions(string email, string additionalCondition)
        {
            List<Session> sessions = new List<Session>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string baseQuery = "SELECT * FROM Session WHERE (MentorEmail = @Email OR MenteeEmail = @Email)";

                if (!string.IsNullOrEmpty(additionalCondition))
                {
                    baseQuery += " " + additionalCondition;
                }

                using (SqlCommand cmd = new SqlCommand(baseQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Session session = new Session(
                                reader.GetDateTime(reader.GetOrdinal("DateTime")),
                                reader.GetString(reader.GetOrdinal("MentorEmail")),
                                reader.GetString(reader.GetOrdinal("MenteeEmail"))
                            );
                            session.Rating = reader.GetInt32(reader.GetOrdinal("Rating"));

                            sessions.Add(session);
                        }
                    }
                }
            }

            return sessions;
        }

        public IEnumerable<Session> GetAllSessions(string email)
        {
            return GetSessions(email, null);
        }

        public IEnumerable<Session> GetUpcomingSessions(string email)
        {
            return GetSessions(email, "AND DateTime > GETDATE()");
        }

        public IEnumerable<Session> GetPastSessions(string email)
        {
            return GetSessions(email, "AND DateTime <= GETDATE()");
        }
    }
}