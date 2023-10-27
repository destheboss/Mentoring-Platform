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
                    string query = "SELECT * FROM Announcement";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Announcement announcement = new Announcement
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = reader["Title"].ToString(),
                                    Message = new StringBuilder(reader["Message"].ToString()),
                                    CreatedBy = reader["CreatedBy"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    Type = (AnnouncementType)Enum.Parse(typeof(AnnouncementType), reader["Type"].ToString())
                                };
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
    }
}