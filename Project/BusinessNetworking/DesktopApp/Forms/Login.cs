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

namespace DesktopApp.Forms
{
    public partial class Login : Form
    {
        private UserManager userManager;
        private SessionManager sessionManager;

        public Login(UserManager userManager, SessionManager sessionManager)
        {
            this.userManager = userManager;
            this.sessionManager = sessionManager;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool[] checkResults = userManager.CheckCredentialsForAdmin(tbUsername.Text, tbPassword.Text);

            if (checkResults[0] == false && checkResults[1] == true)
            {
                MessageBox.Show("You are not authorized!");
            }
            else if (checkResults[0] == true && checkResults[1] == true)
            {
                MessageBox.Show("Successfully logged in.");
            }
            else
            {
                MessageBox.Show("Wrong email or password!");
            }
        }
    }
}
