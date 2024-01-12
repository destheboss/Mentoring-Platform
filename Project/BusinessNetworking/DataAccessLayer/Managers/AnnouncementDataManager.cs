using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;

namespace DataAccessLayer.Managers
{
    public class AnnouncementDataManager : ConnectionSQL, IAnnouncementDataAccess
    {
        public bool CreateAnnouncement(Announcement announcement)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Announcement (Title, Message, CreatedBy, CreatedAt, Type) VALUES (@Title, @Message, @CreatedBy, @CreatedAt, @Type)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", announcement.Title);
                        command.Parameters.AddWithValue("@Message", announcement.Message.ToString());
                        command.Parameters.AddWithValue("@CreatedBy", announcement.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedAt", announcement.CreatedAt);
                        command.Parameters.AddWithValue("@Type", announcement.Type.ToString());

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating the announcement: {ex.Message}");
            }
        }

        public bool DeleteAnnouncement(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Announcement WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting the announcement: {ex.Message}");
            }
        }

        public List<Announcement> GetAllAnnouncements()
        {
            List<Announcement> announcements = new List<Announcement>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Announcement ORDER BY CreatedAt DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string title = reader["Title"].ToString();
                                string message = reader["Message"].ToString();
                                string createdBy = reader["CreatedBy"].ToString();
                                DateTime createdAt = Convert.ToDateTime(reader["CreatedAt"]);
                                DateTime? updatedAt = reader["UpdatedAt"] != DBNull.Value
                                                      ? Convert.ToDateTime(reader["UpdatedAt"])
                                                      : (DateTime?)null;
                                AnnouncementType type = (AnnouncementType)Enum.Parse(typeof(AnnouncementType), reader["Type"].ToString());

                                Announcement announcement = new Announcement(id, title, message, createdBy, createdAt, updatedAt, type);
                                announcements.Add(announcement);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting the announcements: {ex.Message}");
            }
            return announcements;
        }

        public bool UpdateAnnouncement(int id, string newContent)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Announcement SET Message = @NewContent, UpdatedAt = @UpdatedAt WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewContent", newContent);
                        command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating the announcement: {ex.Message}");
            }
        }

        public Announcement? GetLatestAnnouncement()
        {
            Announcement? latestAnnouncement = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT TOP 1 * FROM Announcement ORDER BY CreatedAt DESC";

                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                latestAnnouncement = new Announcement(
                                    (int)reader["Id"],
                                    reader["Title"].ToString(),
                                    reader["Message"].ToString(),
                                    reader["CreatedBy"].ToString(),
                                    (DateTime)reader["CreatedAt"],
                                    reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : (DateTime)reader["UpdatedAt"],
                                    (AnnouncementType)Enum.Parse(typeof(AnnouncementType), reader["Type"].ToString())
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting the latest announcement: {ex.Message}");
            }
            return latestAnnouncement;
        }
    }
}