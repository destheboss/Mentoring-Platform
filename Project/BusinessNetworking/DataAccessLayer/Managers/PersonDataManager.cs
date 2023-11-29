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
using System.Transactions;
using System.Data.Common;

namespace DataAccessLayer.Managers
{
    public class PersonDataManager : ConnectionSQL, IPersonDataAccess
    {
        private readonly IMeetingDataAccess meetingDataAccess;

        public PersonDataManager(IMeetingDataAccess meetingDataAccess)
        {
            this.meetingDataAccess = meetingDataAccess;
        }

        public void AddPerson(User user)
        {
            try
            {
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
                        command.Parameters.AddWithValue("@PasswordHash", Convert.FromBase64String(user.PasswordHash));
                        command.Parameters.AddWithValue("@PasswordSalt", Convert.FromBase64String(user.PasswordSalt));
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

                        string specialtyQuery = "INSERT INTO MentorSpecialty (MentorId, SpecialtyId) VALUES (@MentorId, @SpecialtyId)";
                        foreach (var specialty in mentor.Specialties)
                        {
                            using (SqlCommand command = new SqlCommand(specialtyQuery, connection))
                            {
                                command.Parameters.AddWithValue("@MentorId", userId);
                                command.Parameters.AddWithValue("@SpecialtyId", specialty);
                                command.ExecuteNonQuery();
                            }
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
                            string deleteMentorSpecialtiesQuery = "DELETE FROM MentorSpecialty WHERE MentorId IN (SELECT Id FROM Person WHERE Email = @Email)";
                            using (SqlCommand command = new SqlCommand(deleteMentorSpecialtiesQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Email", user.Email);
                                command.ExecuteNonQuery();
                            }

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
                        var specialties = GetMentorSpecialties(reader.GetInt32(reader.GetOrdinal("Id")));
                        return new Mentor(id, firstName, lastName, email, role, isActive, rating, specialties, imagePath);

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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

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
                }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
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
                }
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
                                var specialties = GetMentorSpecialties(reader.GetInt32(reader.GetOrdinal("Id")));
                                Mentor mentor = new Mentor(id, firstName, lastName, email, role, isActive, rating, specialties, image);
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

        public bool UpdatePersonInfo(User oldUser, User updatedUser)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        string personQuery = @"
                UPDATE Person SET
                FirstName = @newFirstName,
                LastName = @newLastName,
                Email = @newEmail,
                PasswordHash = @newPasswordHash,
                PasswordSalt = @newPasswordSalt,
                Role = @newRole
                WHERE Id = @Id";

                        using (SqlCommand command = new SqlCommand(personQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@newFirstName", updatedUser.FirstName);
                            command.Parameters.AddWithValue("@newLastName", updatedUser.LastName);
                            command.Parameters.AddWithValue("@newEmail", updatedUser.Email);

                            var passwordHashBytes = Convert.FromBase64String(updatedUser.PasswordHash);
                            var passwordSaltBytes = Convert.FromBase64String(updatedUser.PasswordSalt);

                            command.Parameters.AddWithValue("@newPasswordHash", passwordHashBytes);
                            command.Parameters.AddWithValue("@newPasswordSalt", passwordSaltBytes);
                            command.Parameters.AddWithValue("@newRole", updatedUser.Role.ToString());
                            command.Parameters.AddWithValue("@Id", oldUser.Id);

                            command.ExecuteNonQuery();
                        }

                        if (updatedUser is Mentor updatedMentor)
                        {
                            string deleteSpecialtiesQuery = "DELETE FROM MentorSpecialty WHERE MentorId = @MentorId";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteSpecialtiesQuery, connection, transaction))
                            {
                                deleteCommand.Parameters.AddWithValue("@MentorId", oldUser.Id);
                                deleteCommand.ExecuteNonQuery();
                            }

                            foreach (var specialty in updatedMentor.Specialties)
                            {
                                string insertSpecialtyQuery = "INSERT INTO MentorSpecialty (MentorId, SpecialtyId) VALUES (@MentorId, @SpecialtyId)";
                                using (SqlCommand insertCommand = new SqlCommand(insertSpecialtyQuery, connection, transaction))
                                {
                                    insertCommand.Parameters.AddWithValue("@MentorId", oldUser.Id);
                                    insertCommand.Parameters.AddWithValue("@SpecialtyId", (int)specialty);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }

                    return true;
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

        private List<Specialty> GetMentorSpecialties(int mentorId)
        {
            var specialties = new List<Specialty>();
            string query = @"
        SELECT s.Name 
        FROM MentorSpecialty ms
        JOIN Specialty s ON ms.SpecialtyId = s.Id
        WHERE ms.MentorId = @MentorId";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MentorId", mentorId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var specialtyName = reader.GetString(0);
                            if (Enum.TryParse<Specialty>(specialtyName, out var specialty))
                            {
                                specialties.Add(specialty);
                            }
                            else
                            {
                                throw new Exception("Database error occured while extracting the mentor's specialties.");
                            }
                        }
                    }
                }
            }
            return specialties;
        }
    }
}