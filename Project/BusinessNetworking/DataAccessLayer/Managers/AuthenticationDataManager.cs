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
        private readonly HashingManager hashingManager;

        public AuthenticationDataManager(HashingManager hashingManager)
        {
            this.hashingManager = hashingManager;
        }

        public bool[] CheckCredentialsForAdmin(string email, string password)
        {
            bool[] result = new bool[] { false, false };
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
                                bool isVerified = hashingManager.VerifyHash(password, storedHash, storedSalt);
                                string role = reader["Role"].ToString();

                                if (role == "Admin")
                                {
                                    result = new bool[] { true, isVerified };
                                }
                                else
                                {
                                    result = new bool[] { false, isVerified };
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the person's credentials: {ex.Message}", ex);
            }
        }

        public bool CheckCredentialsForUser(string email, string password)
        {
            bool isVerified = false;
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
                                isVerified = hashingManager.VerifyHash(password, storedHash, storedSalt);
                            }
                        }
                    }
                }
                return isVerified;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the person's credentials: {ex.Message}", ex);
            }
        }
    }
}