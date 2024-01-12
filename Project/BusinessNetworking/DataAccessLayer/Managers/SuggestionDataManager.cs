using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Managers
{
    public class SuggestionDataManager : ConnectionSQL, ISuggestionDataAccess
    {
        public IEnumerable<int> GetSuggestionHistory(int userId)
        {
            var suggestedMentorIds = new List<int>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT MentorId 
                    FROM SuggestionHistory 
                    WHERE UserId = @UserId 
                    ORDER BY SuggestedAt DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                suggestedMentorIds.Add((int)reader["MentorId"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while retrieving suggestion history: {ex.Message}", ex);
            }

            return suggestedMentorIds;
        }

        public void AddSuggestions(int userId, IEnumerable<int> mentorIds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var mentorId in mentorIds)
                    {
                        string query = @"
                    INSERT INTO SuggestionHistory (UserId, MentorId)
                    VALUES (@UserId, @MentorId)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@MentorId", mentorId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while adding suggestions: {ex.Message}", ex);
            }
        }

        public void ClearPreviousSuggestions(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM SuggestionHistory WHERE UserId = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while clearing previous suggestions: {ex.Message}", ex);
            }
        }
    }
}