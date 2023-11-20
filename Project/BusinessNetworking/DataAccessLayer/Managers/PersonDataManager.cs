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
        private readonly PasswordStrengthChecker passwordStrengthChecker;

        public PersonDataManager(IMeetingDataAccess meetingDataAccess, HashingManager hashingManager, PasswordStrengthChecker passwordStrengthChecker)
        {
            this.meetingDataAccess = meetingDataAccess;
            this.hashingManager = hashingManager;
            this.passwordStrengthChecker = passwordStrengthChecker;
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

        public void AddPerson(User user)
        {
            try
            {
                if (!passwordStrengthChecker.IsPasswordStrong(user.Password))
                {
                    throw new ArgumentException("Password does not meet the strength requirements.");
                }

                var (hash, salt) = hashingManager.GenerateHashWithSalt(user.Password);

                string personQuery = @"
            INSERT INTO Person (FirstName, LastName, Email, PasswordHash, PasswordSalt, Role, IsActive, Image) 
            OUTPUT INSERTED.Id 
            VALUES (@FirstName, @LastName, @Email, @PasswordHash, @PasswordSalt, @Role, @IsActive, @Image)";

                int userId;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(personQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@PasswordHash", hash);
                        command.Parameters.AddWithValue("@PasswordSalt", salt);
                        command.Parameters.AddWithValue("@Role", user.Role.ToString());
                        command.Parameters.AddWithValue("@IsActive", user.isActive);
                        command.Parameters.AddWithValue("@Image", (object)user.Image ?? DBNull.Value);

                        userId = (int)command.ExecuteScalar();
                    }

                    if (user is Mentor mentor)
                    {
                        string mentorQuery = "INSERT INTO Mentor (PersonId, Rating) VALUES (@PersonId, @Rating)";
                        using (SqlCommand command = new SqlCommand(mentorQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PersonId", userId);
                            command.Parameters.AddWithValue("@Rating", mentor.Rating);
                            command.ExecuteNonQuery();
                        }
                    }
                    else if (user is Mentee)
                    {
                        string menteeQuery = "INSERT INTO Mentee (PersonId) VALUES (@PersonId)";
                        using (SqlCommand command = new SqlCommand(menteeQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PersonId", userId);
                            command.ExecuteNonQuery();
                        }
                    }
                    else if (user is Admin)
                    {
                        string adminQuery = "INSERT INTO Admin (PersonId) VALUES (@PersonId)";
                        using (SqlCommand command = new SqlCommand(adminQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PersonId", userId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while adding the user: {ex.Message}", ex);
            }
        }

        public void RemovePerson(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string roleSpecificQuery = "";
                    switch (user.Role)
                    {
                        case Role.Mentor:
                            roleSpecificQuery = "DELETE FROM Mentor WHERE PersonId IN (SELECT Id FROM Person WHERE Email = @Email)";
                            break;
                        case Role.Mentee:
                            roleSpecificQuery = "DELETE FROM Mentee WHERE PersonId IN (SELECT Id FROM Person WHERE Email = @Email)";
                            break;
                        case Role.Admin:
                            roleSpecificQuery = "DELETE FROM Admin WHERE PersonId IN (SELECT Id FROM Person WHERE Email = @Email)";
                            break;
                        default:
                            throw new ArgumentException("Invalid role type.");
                    }

                    using (SqlCommand roleSpecificCommand = new SqlCommand(roleSpecificQuery, connection))
                    {
                        roleSpecificCommand.Parameters.AddWithValue("@Email", user.Email);
                        roleSpecificCommand.ExecuteNonQuery();
                    }

                    string personQuery = "DELETE FROM Person WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(personQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", user.Email);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Person not found in the database.");
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(user.Image))
                            {
                                string imagePath = Path.Combine("wwwroot", user.Image);
                                if (File.Exists(imagePath))
                                {
                                    File.Delete(imagePath);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while removing the user: {ex.Message}", ex);
            }
        }

        private IPerson CreatePersonFromReader(SqlDataReader reader, Role role)
        {
            try
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                string email = reader["Email"].ToString();
                string imagePath = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader["Image"].ToString();
                bool isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                switch (role)
                {
                    case Role.Admin:
                        return new Admin(id, firstName, lastName, email, role, isActive, imagePath);

                    case Role.Mentor:
                        float rating = role == Role.Mentor && !reader.IsDBNull(reader.GetOrdinal("Rating"))
                                       ? (float)reader.GetDouble(reader.GetOrdinal("Rating"))
                                       : 0f;
                        return new Mentor(id, firstName, lastName, email, role, isActive, rating, imagePath);

                    case Role.Mentee:
                        return new Mentee(id, firstName, lastName, email, role, isActive, imagePath);

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
                    //string query = "SELECT * FROM Person WHERE Role = @Role";
                    string query = role == Role.Mentor
               ? "SELECT p.*, m.Rating FROM Person p LEFT JOIN Mentor m ON p.Id = m.PersonId WHERE p.Role = @Role"
               : "SELECT * FROM Person WHERE Role = @Role";
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
                throw new Exception($"Database error occurred while extracting the user's data: {ex.Message}", ex);
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

        public IPerson? GetPersonByEmail(string email)
        {
            IPerson? resultPerson = null;
            try
            {
                ExecuteQuery(connection =>
                {
                    string query = @"
            SELECT p.Id, p.FirstName, p.LastName, p.Email, p.Role, p.IsActive, p.Image, m.Rating 
            FROM Person p
            LEFT JOIN Mentor m ON p.Id = m.PersonId
            WHERE p.Email = @Email";

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
                throw new Exception($"Database error occurred while extracting the user's data: {ex.Message}", ex);
            }
        }

        public List<IPerson> GetAllPersons()
        {
            List<IPerson> persons = new List<IPerson>();

            string sqlQuery = @"
        SELECT p.Id, p.FirstName, p.LastName, p.Email, p.Role, p.IsActive, p.Image,
               m.Rating AS MentorRating
        FROM Person p
        LEFT JOIN Mentor m ON p.Id = m.PersonId
        LEFT JOIN Mentee me ON p.Id = me.PersonId
        LEFT JOIN Admin a ON p.Id = a.PersonId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        string email = reader.GetString(reader.GetOrdinal("Email"));
                        Role role = ConvertToRole(reader.GetString(reader.GetOrdinal("Role")));
                        bool isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        string image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString(reader.GetOrdinal("Image"));

                        switch (role)
                        {
                            case Role.Mentor:
                                float rating = reader.IsDBNull(reader.GetOrdinal("MentorRating")) ? 0f : (float)reader.GetDouble(reader.GetOrdinal("MentorRating"));
                                Mentor mentor = new Mentor(id, firstName, lastName, email, role, isActive, rating, image);
                                persons.Add(mentor);
                                break;
                            case Role.Mentee:
                                Mentee mentee = new Mentee(id, firstName, lastName, email, role, isActive, image);
                                persons.Add(mentee);
                                break;
                            case Role.Admin:
                                Admin admin = new Admin(id, firstName, lastName, email, role, isActive, image);
                                persons.Add(admin);
                                break;
                        }
                    }
                }
            }

            return persons;
        }

        private Role ConvertToRole(string roleStr)
        {
            if (Enum.TryParse(roleStr, true, out Role role))
            {
                return role;
            }
            else
            {
                throw new ArgumentException($"Invalid role string: {roleStr}");
            }
        }

        public bool UpdatePersonInfo(IPerson person, string newFirstName, string newLastName, string newEmail, string newPassword, Role newRole)
        {
            var (hash, salt) = hashingManager.GenerateHashWithSalt(newPassword);

            try
            {
                string query = @"
            UPDATE Person SET
            FirstName = @newFirstName,
            LastName = @newLastName,
            Email = @newEmail,
            PasswordHash = @newPasswordHash,
            PasswordSalt = @newPasswordSalt,
            Role = @newRole
            WHERE Id = @Id";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newFirstName", newFirstName);
                        command.Parameters.AddWithValue("@newLastName", newLastName);
                        command.Parameters.AddWithValue("@newEmail", newEmail);
                        command.Parameters.AddWithValue("@newPasswordHash", hash);
                        command.Parameters.AddWithValue("@newPasswordSalt", salt);
                        command.Parameters.AddWithValue("@newRole", newRole.ToString());
                        command.Parameters.AddWithValue("@Id", person.Id);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while updating the user's info: {ex.Message}", ex);
            }
        }

        public void UpdateMentorAverageRating(Mentor mentor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string updateQuery = @"
                UPDATE Mentor 
                SET Rating = @Rating 
                WHERE PersonId = (SELECT Id FROM Person WHERE Email = @Email)";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rating", mentor.Rating);
                        cmd.Parameters.AddWithValue("@Email", mentor.Email);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Mentor not found or update failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while updating the mentor's rating: {ex.Message}", ex);
            }
        }
    }
}