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

namespace DesktopApp.Forms
{
    public partial class Login : Form
    {
        private UserManager _userManager;
        public Login()
        {
            InitializeComponent();
            _userManager = new UserManager();

            // For now everything is hardcoded but will soon make use of a database.
            Admin admin = new Admin("Admin", "admin@gmail.com", "123", Role.Admin);
            Mentor mentor = new Mentor("John", "john@gmail.com", "123", Role.Mentor);
            _userManager.AddPerson(admin);
            _userManager.AddPerson(mentor);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool[] checkResults = _userManager.CheckCredentialsForAdmin(tbUsername.Text, tbPassword.Text);

            if (!checkResults[0])
            {
                MessageBox.Show("Wrong email or password!");
            }
            else if (!checkResults[1])
            {
                MessageBox.Show("You are not authorized!");
            }
            else
            {
                MessageBox.Show("Successfully logged in.");
            }
        }
    }
}
