using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Managers;
using System.Data;

namespace DataAccessLayer.Managers
{
    public class PersonDataManager : ConnectionSQL, IPersonDataAccess
    {
        private readonly IMeetingDataAccess meetingDataAccess;
        private readonly HashingManager hashingManager;

        public PersonDataManager(IMeetingDataAccess meetingDataAccess, HashingManager hashingManager)
        {
            this.meetingDataAccess = meetingDataAccess;
            this.hashingManager = hashingManager;
        }

        private void ExecuteQuery(Action<SqlConnection> action)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                action(connection);
            }
        }

        private void ExecuteNonQuery(Action<SqlConnection> action)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                action(connection);
            }
        }

        public void AddPerson(IPerson person)
        {
            try
            {
                var (hash, salt) = hashingManager.GenerateHashWithSalt(person.Password);

                ExecuteNonQuery(connection =>
                {
                    string query = "INSERT INTO Person (FirstName, LastName, Email, PasswordHash, PasswordSalt, Role, Rating, IsActive) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @PasswordSalt, @Role, @Rating, @IsActive)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@Email", person.Email);
                        command.Parameters.AddWithValue("@PasswordHash", hash);
                        command.Parameters.AddWithValue("@PasswordSalt", salt);
                        command.Parameters.AddWithValue("@Role", person.Role.ToString());

                        if (person is Mentor mentor)
                        {
                            command.Parameters.AddWithValue("@Rating", mentor.Rating);
                            command.Parameters.AddWithValue("@IsActive", mentor.isActive);
                        }
                        else if (person is Mentee mentee)
                        {
                            command.Parameters.AddWithValue("@Rating", DBNull.Value);
                            command.Parameters.AddWithValue("@IsActive", mentee.isActive);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Rating", DBNull.Value);
                            command.Parameters.AddWithValue("@IsActive", DBNull.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while adding the person: {ex.Message}", ex);
            }
        }

        public void RemovePerson(IPerson person)
        {
            try
            {
                ExecuteNonQuery(connection =>
                {
                    string query = "DELETE FROM Person WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", person.Email);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Person not found in the database.");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while removing the person: {ex.Message}", ex);
            }
        }

        private void UpdateUserActivityStatus(User user, bool isActive)
        {
            try
            {
                ExecuteNonQuery(connection =>
                {
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
                });
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

        public void UpdateRating(Mentor mentor)
        {
            try
            {
                IEnumerable<Meeting> mentorMeetings = meetingDataAccess.GetAllMeetings(mentor.Email);

                double averageRating = 0;
                int count = 0;
                foreach (Meeting meeting in mentorMeetings)
                {
                    if (meeting.Rating > 0) // Assuming that 0 means 'unrated'
                    {
                        averageRating += meeting.Rating;
                        count++;
                    }
                }

                if (count > 0)
                {
                    averageRating = averageRating / count;
                }

                ExecuteNonQuery(connection =>
                {
                    string query = "UPDATE Mentor SET Rating = @NewRating WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@NewRating", averageRating);
                        cmd.Parameters.AddWithValue("@Email", mentor.Email);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Mentor not found in the database.");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while updating the person's rating: {ex.Message}", ex);
            }
        }

        private IPerson CreatePersonFromReader(SqlDataReader reader, Role role)
        {
            try
            {
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                string email = reader["Email"].ToString();
                bool? isActive = reader["IsActive"] as bool?;
                float? rating = reader["Rating"] as float?;

                switch (role)
                {
                    case Role.Admin:
                        return new Admin(firstName, lastName, email, role);

                    case Role.Mentor:
                        Mentor mentor = new Mentor(firstName, lastName, email, role, isActive ?? false, rating ?? 0);
                        return mentor;

                    case Role.Mentee:
                        Mentee mentee = new Mentee(firstName, lastName, email, role, isActive ?? false);
                        return mentee;

                    default:
                        throw new ArgumentException("Invalid role type.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while extracting the person's data: {ex.Message}", ex);
            }
        }

        private List<IPerson> GetPersons(Role role)
        {
            List<IPerson> persons = new List<IPerson>();
            try
            {
                ExecuteQuery(connection =>
                {
                    string query = "SELECT * FROM Person WHERE Role = @Role";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Role", role.ToString());

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IPerson person = CreatePersonFromReader(reader, role);
                                persons.Add(person);
                            }
                        }
                    }
                });
                return persons;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while extracting the person's data: {ex.Message}", ex);
            }
        }

        public IEnumerable<Admin> GetAdmins()
        {
            var persons = GetPersons(Role.Admin);
            return persons.Cast<Admin>();
        }

        public IEnumerable<Mentor> GetMentors()
        {
            var persons = GetPersons(Role.Mentor);
            return persons.Cast<Mentor>();
        }

        public IEnumerable<Mentee> GetMentees()
        {
            var persons = GetPersons(Role.Mentee);
            return persons.Cast<Mentee>();
        }

        public bool[] CheckCredentialsForAdmin(string email, string password)
        {
            bool[] result = new bool[] { false, false };
            try
            {
                ExecuteQuery(connection =>
                {
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
                });
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
                ExecuteQuery(connection =>
                {
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
                });
                return isVerified;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the person's credentials: {ex.Message}", ex);
            }
        }

        public IPerson? GetPersonByEmail(string email)
        {
            IPerson? resultPerson = null;
            try
            {
                ExecuteQuery(connection =>
                {
                    string query = "SELECT * FROM Person WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Role role = (Role)Enum.Parse(typeof(Role), reader["Role"].ToString());
                                resultPerson = CreatePersonFromReader(reader, role);
                            }
                        }
                    }
                });
                return resultPerson;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while extracting the person's data: {ex.Message}", ex);
            }
        }
    }
}