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
    }
}