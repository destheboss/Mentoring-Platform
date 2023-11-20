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

                string query = @"
            INSERT INTO Meeting (DateTime, MentorId, MentorEmail, MenteeId, MenteeEmail, Rating) 
            VALUES (@DateTime, @MentorId, @MentorEmail, @MenteeId, @MenteeEmail, @Rating)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DateTime", meeting.DateTime);
                    cmd.Parameters.AddWithValue("@MentorId", meeting.MentorId);
                    cmd.Parameters.AddWithValue("@MentorEmail", meeting.MentorEmail);
                    cmd.Parameters.AddWithValue("@MenteeId", meeting.MenteeId);
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
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetDateTime(reader.GetOrdinal("DateTime")),
                                reader.GetInt32(reader.GetOrdinal("MentorId")),
                                reader.GetString(reader.GetOrdinal("MentorEmail")),
                                reader.GetInt32(reader.GetOrdinal("MenteeId")),
                                reader.GetString(reader.GetOrdinal("MenteeEmail")),
                                reader.GetInt32(reader.GetOrdinal("Rating"))
                            );

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

        public void UpdateMeetingEmails(string oldEmail, string newEmail)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string mentorUpdateQuery = @"
                    UPDATE Meeting 
                    SET MentorEmail = @NewEmail 
                    WHERE MentorEmail = @OldEmail";

                using (SqlCommand mentorCmd = new SqlCommand(mentorUpdateQuery, conn))
                {
                    mentorCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                    mentorCmd.Parameters.AddWithValue("@OldEmail", oldEmail);

                    mentorCmd.ExecuteNonQuery();
                }

                string menteeUpdateQuery = @"
                    UPDATE Meeting 
                    SET MenteeEmail = @NewEmail 
                    WHERE MenteeEmail = @OldEmail";

                using (SqlCommand menteeCmd = new SqlCommand(menteeUpdateQuery, conn))
                {
                    menteeCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                    menteeCmd.Parameters.AddWithValue("@OldEmail", oldEmail);

                    menteeCmd.ExecuteNonQuery();
                }
            }
        }

        public Meeting GetMeetingById(int meetingId)
        {
            Meeting meeting = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Meeting WHERE Id = @MeetingId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MeetingId", meetingId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            meeting = new Meeting(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetDateTime(reader.GetOrdinal("DateTime")),
                                reader.GetInt32(reader.GetOrdinal("MentorId")),
                                reader.GetString(reader.GetOrdinal("MentorEmail")),
                                reader.GetInt32(reader.GetOrdinal("MenteeId")),
                                reader.GetString(reader.GetOrdinal("MenteeEmail")),
                                reader.GetInt32(reader.GetOrdinal("Rating"))
                            );
                        }
                    }
                }
            }

            return meeting;
        }

        public bool UpdateMeetingRating(Meeting? meeting)
        {
            if (meeting == null)
            {
                return false;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                UPDATE Meeting 
                SET Rating = @Rating 
                WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rating", meeting.Rating);
                        cmd.Parameters.AddWithValue("@Id", meeting.Id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while updating the meeting's rating: {ex.Message}", ex);
            }
        }
    }
}