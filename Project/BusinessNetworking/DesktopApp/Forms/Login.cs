using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Models;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using System.Data.SqlClient;

namespace DesktopApp.Forms
{
    public partial class Login : Form
    {
        private UserManager userManager;
        private MeetingManager meetingManager;
        private AuthenticationManager authenticationManager;

        public Login(UserManager userManager, MeetingManager meetingManager, AuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.meetingManager = meetingManager;
            this.authenticationManager = authenticationManager;

            //Admin admin = new Admin("Desislav", "Hristov", "admin@gmail.com", "123", Role.Admin);
            //userManager.AddPerson(admin);

            //GenerateDummyMentors();

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool[] checkResults = authenticationManager.CheckCredentialsForAdmin(tbUsername.Text, tbPassword.Text);

            if (checkResults[0] == false && checkResults[1] == true)
            {
                MessageBox.Show("You are not authorized!");
            }
            else if (checkResults[0] == true && checkResults[1] == true)
            {
                MessageBox.Show("Successfully logged in.");

                IPerson user = userManager.GetPersonByEmail(tbUsername.Text);

                Main form = new Main(user, userManager, meetingManager);
                this.Hide();
                form.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong email or password!");
            }
        }

        public void GenerateDummyMentors()
        {
            int numberOfMentors = 20;
            var random = new Random();
            for (int i = 1; i < numberOfMentors; i++)
            {
                var firstName = $"Mentor{i}";
                var lastName = $"LastName{i}";
                var email = $"mentor{i}@example.com";
                var password = "Password123";
                var role = Role.Mentor;
                var specialties = GetRandomSpecialties();
                var rating = (float)(random.NextDouble() * 5.0);
                var mentor = new Mentor(firstName, lastName, email, password, role, specialties);

                mentor.Rating = rating;
                userManager.AddPerson(mentor);
            }
        }

        private List<Specialty> GetRandomSpecialties()
        {
            var random = new Random();
            var selectedSpecialties = new List<Specialty>();

            var values = Enum.GetValues(typeof(Specialty));
            int numberOfSpecialties = random.Next(1, 3); // Either 1 or 2 specialties

            for (int i = 0; i < numberOfSpecialties; i++)
            {
                Specialty randomSpecialty;
                do
                {
                    randomSpecialty = (Specialty)values.GetValue(random.Next(values.Length));
                }
                while (selectedSpecialties.Contains(randomSpecialty)); // Ensure no duplicates

                selectedSpecialties.Add(randomSpecialty);
            }

            return selectedSpecialties;
        }
    }
}