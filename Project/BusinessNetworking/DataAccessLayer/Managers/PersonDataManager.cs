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
        private readonly ISessionDataAccess sessionDataAccess;
        private readonly HashingManager hashingManager;

        public PersonDataManager(ISessionDataAccess sessionDataAccess, HashingManager hashingManager)
        {
            this.sessionDataAccess = sessionDataAccess;
            this.hashingManager = hashingManager;
        }

        public void AddPerson(IPerson person)
        {
            var (hash, salt) = hashingManager.GenerateHashWithSalt(person.Password);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Person (Name, Email, PasswordHash, PasswordSalt, Role, Rating, IsActive) VALUES (@Name, @Email, @PasswordHash, @PasswordSalt, @Role, @Rating, @IsActive)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", person.Name);
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
            }
        }

        public void RemovePerson(IPerson person)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

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
            }
        }

        private void UpdateUserActivityStatus(User user, bool isActive)
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
            IEnumerable<Session> mentorSessions = sessionDataAccess.GetAllSessions(mentor.Email);

            double averageRating = 0;
            int count = 0;
            foreach (Session session in mentorSessions)
            {
                if (session.Rating > 0) // Assuming that 0 means 'unrated'
                {
                    averageRating += session.Rating;
                    count++;
                }
            }

            if (count > 0)
            {
                averageRating = averageRating / count;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Mentor SET Rating = @NewRating WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewRating", averageRating);
                    cmd.Parameters.AddWithValue("@Email", mentor.Email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOperationException("Mentor not found in the database.");
                    }
                }
            }
        }

        private IPerson CreatePersonFromReader(SqlDataReader reader, Role role)
        {
            string name = reader["Name"].ToString();
            string email = reader["Email"].ToString();
            bool? isActive = reader["IsActive"] as bool?;
            float? rating = reader["Rating"] as float?;

            switch (role)
            {
                case Role.Admin:
                    return new Admin(name, email, role);

                case Role.Mentor:
                    Mentor mentor = new Mentor(name, email, role, isActive ?? false, rating ?? 0);
                    return mentor;

                case Role.Mentee:
                    Mentee mentee = new Mentee(name, email, role, isActive ?? false);
                    return mentee;

                default:
                    throw new ArgumentException("Invalid role type.");
            }
        }

        private List<IPerson> GetPersons(Role role)
        {
            List<IPerson> persons = new List<IPerson>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Person WHERE Role = @Role";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Role", role);

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT PasswordHash, PasswordSalt, Role FROM Person WHERE Email=@Email", connection);
                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    byte[] storedHash = (byte[])reader["PasswordHash"];
                    byte[] storedSalt = (byte[])reader["PasswordSalt"];
                    bool isVerified = hashingManager.VerifyHash(password, storedHash, storedSalt);
                    string role = reader["Role"].ToString();

                    if (role == "Admin")
                    {
                        return new bool[] { true, isVerified }; // IsAdmin, CredentialsAreCorrect
                    }
                    else
                    {
                        return new bool[] { false, isVerified };
                    }
                }
                else
                {
                    return new bool[] { false, false }; // IsAdmin, CredentialsAreCorrect
                }
            }
        }

        public bool[] CheckCredentialsForUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT PasswordHash, PasswordSalt, Role FROM Person WHERE Email=@Email", connection);
                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    byte[] storedHash = (byte[])reader["PasswordHash"];
                    byte[] storedSalt = (byte[])reader["PasswordSalt"];
                    bool isVerified = hashingManager.VerifyHash(password, storedHash, storedSalt);
                    string role = reader["Role"].ToString();

                    return new bool[] { role != "Admin", isVerified };
                }
                else
                {
                    return new bool[] { false, false }; // IsUser, CredentialsAreCorrect
                }
            }
        }

        public IPerson? GetPersonByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE Email=@Email", connection);
                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Role role = (Role)Enum.Parse(typeof(Role), reader["Role"].ToString());
                    IPerson person = CreatePersonFromReader(reader, role);

                    return person;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}