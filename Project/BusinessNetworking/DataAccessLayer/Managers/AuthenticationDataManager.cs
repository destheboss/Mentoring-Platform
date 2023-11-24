using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Managers
{
    public class AuthenticationDataManager : ConnectionSQL, IAuthenticationDataAccess
    {
        public (byte[] passwordHash, byte[] passwordSalt, string role) GetUserCredentials(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT PasswordHash, PasswordSalt, Role FROM Person WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] storedHash = (byte[])reader["PasswordHash"];
                                byte[] storedSalt = (byte[])reader["PasswordSalt"];
                                string role = reader["Role"].ToString();

                                return (storedHash, storedSalt, role);
                            }
                        }
                    }
                }
                return (null, null, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user's credentials: {ex.Message}", ex);
            }
        }
    }
}