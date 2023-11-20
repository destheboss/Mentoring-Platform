using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using System.Data;
using BusinessLogicLayer.Common;
using System.Data.SqlClient;

namespace DataAccessLayer.Managers
{
    public class UserActionsDataManager : ConnectionSQL, IUserActionsDataAccess
    {
        private void UpdateUserActivityStatus(User user, bool isActive)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Person SET IsActive = @IsActive WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IsActive", isActive);
                        command.Parameters.AddWithValue("@Email", user.Email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("User not found in the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while updating the person's activity: {ex.Message}", ex);
            }
        }

        public void SuspendUser(User user)
        {
            UpdateUserActivityStatus(user, false);
        }

        public void UnsuspendUser(User user)
        {
            UpdateUserActivityStatus(user, true);
        }
    }
}