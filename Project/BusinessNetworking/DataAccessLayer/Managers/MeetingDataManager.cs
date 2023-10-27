using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;

namespace DataAccessLayer.Managers
{
    public class MeetingDataManager : ConnectionSQL, IMeetingDataAccess
    {
        public void AddMeeting(Meeting meeting)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO Meeting (DateTime, MentorEmail, MenteeEmail, Rating) VALUES (@DateTime, @MentorEmail, @MenteeEmail, @Rating)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DateTime", meeting.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", meeting.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", meeting.MenteeEmail);
                    cmd.Parameters.AddWithValue("@Rating", meeting.Rating);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CancelMeeting(Meeting meeting)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Meeting WHERE DateTime = @DateTime AND MentorEmail = @MentorEmail AND MenteeEmail = @MenteeEmail AND Rating = @Rating";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DateTime", meeting.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", meeting.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", meeting.MenteeEmail);
                    cmd.Parameters.AddWithValue("@Rating", meeting.Rating);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Meeting not found in the database.");
                    }
                }
            }
        }

        public void ChangeMeetingTime(Meeting meeting, DateTime newDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Meeting SET DateTime = @NewDateTime WHERE DateTime = @OldDateTime AND MentorEmail = @MentorEmail AND MenteeEmail = @MenteeEmail";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewDateTime", newDate);

                    cmd.Parameters.AddWithValue("@OldDateTime", meeting.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", meeting.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", meeting.MenteeEmail);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Meeting not found in the database.");
                    }
                }
            }
        }

        public void RateMeeting(Meeting meeting, int newMeetingRating)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Meeting SET Rating = @NewRating WHERE DateTime = @DateTime AND MentorEmail = @MentorEmail AND MenteeEmail = @MenteeEmail";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewRating", newMeetingRating);

                    cmd.Parameters.AddWithValue("@DateTime", meeting.DateTime);
                    cmd.Parameters.AddWithValue("@MentorEmail", meeting.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeEmail", meeting.MenteeEmail);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Meeting not found in the database.");
                    }
                }
            }
        }

        private IEnumerable<Meeting> GetMeetings(string email, string additionalCondition)
        {
            List<Meeting> meetings = new List<Meeting>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string baseQuery = "SELECT * FROM Meeting WHERE (MentorEmail = @Email OR MenteeEmail = @Email)";

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
                            Meeting meeting = new Meeting(
                                reader.GetDateTime(reader.GetOrdinal("DateTime")),
                                reader.GetString(reader.GetOrdinal("MentorEmail")),
                                reader.GetString(reader.GetOrdinal("MenteeEmail"))
                            );
                            meeting.Rating = reader.GetInt32(reader.GetOrdinal("Rating"));

                            meetings.Add(meeting);
                        }
                    }
                }
            }

            return meetings;
        }

        public IEnumerable<Meeting> GetAllMeetings(string email)
        {
            return GetMeetings(email, null);
        }

        public IEnumerable<Meeting> GetUpcomingMeetings(string email)
        {
            return GetMeetings(email, "AND DateTime > GETDATE()");
        }

        public IEnumerable<Meeting> GetPastMeetings(string email)
        {
            return GetMeetings(email, "AND DateTime <= GETDATE()");
        }
    }
}