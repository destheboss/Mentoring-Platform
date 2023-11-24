using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Models;
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
    public partial class UsersEdit : Form
    {
        private UserManager userManager;
        private Users usersForm;
        private User selectedUser;
        public UsersEdit(UserManager userManager, Users usersForm, User selectedUser)
        {
            this.userManager = userManager;
            this.usersForm = usersForm;
            this.selectedUser = selectedUser;
            InitializeComponent();
        }

        private void UsersEdit_Load(object sender, EventArgs e)
        {
            this.WindowState = usersForm.WindowState;

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                cbxRoles.Items.Add(role);
            }

            tbxNewName.Text = selectedUser.FirstName;
            tbxNewLastName.Text = selectedUser.LastName;
            tbxNewEmail.Text = selectedUser.Email;
            cbxRoles.SelectedItem = selectedUser.Role;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string newFirstName = tbxNewName.Text;
                string newLastName = tbxNewLastName.Text;
                string newEmail = tbxNewEmail.Text;
                string newPassword = tbxNewPassword.Text;
                Role newRole = (Role)cbxRoles.SelectedItem;

                User updatedUser;

                switch (newRole)
                {
                    case Role.Admin:
                        updatedUser = new Admin(newFirstName, newLastName, newEmail, newPassword, newRole);
                        break;
                    case Role.Mentor:
                        updatedUser = new Mentor(newFirstName, newLastName, newEmail, newPassword, newRole);
                        break;
                    case Role.Mentee:
                        updatedUser = new Mentee(newFirstName, newLastName, newEmail, newPassword, newRole);
                        break;
                    default:
                        throw new InvalidOperationException("Unsupported role type.");
                }

                bool updateSuccessful = userManager.UpdatePersonInfo(selectedUser, updatedUser);

                if (updateSuccessful)
                {
                    MessageBox.Show("Person information updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update failed. Please check the details and try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}