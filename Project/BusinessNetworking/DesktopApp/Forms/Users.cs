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
    public partial class Users : Form
    {
        private IPerson loggedAdmin;
        private MeetingManager meetingManager;
        private UserManager userManager;
        private Main mainForm;
        public Users(IPerson admin, MeetingManager meetingManager, UserManager userManager, Main mainForm)
        {
            this.loggedAdmin = admin;
            this.meetingManager = meetingManager;
            this.userManager = userManager;
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            this.WindowState = mainForm.WindowState;
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                cbxRoles.Items.Add(role);
            }

            lbxUsers.Items.Clear();

            foreach (var person in userManager.GetMentors())
            {
                lbxUsers.Items.Add(person);
            }
            foreach (var person in userManager.GetMentees())
            {
                lbxUsers.Items.Add(person);
            }
            foreach (var person in userManager.GetAdmins())
            {
                lbxUsers.Items.Add(person);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRoleSelected())
                {
                    MessageBox.Show("Please select a role.");
                    return;
                }

                var role = GetSelectedRole();
                var person = CreatePersonByRole(role);

                if (person == null)
                {
                    MessageBox.Show("Invalid role or unable to create person.");
                    return;
                }

                AddPersonToUserManager(person);

                MessageBox.Show("User successfully created!");
                tbxFirstName.Text = null;
                tbxLastName.Text = null;
                tbxEmail.Text = null;
                tbxPassword.Text = null;
                cbxRoles.Text = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsRoleSelected()
        {
            return cbxRoles.SelectedItem != null;
        }

        private Role GetSelectedRole()
        {
            return (Role)cbxRoles.SelectedItem;
        }

        private IPerson CreatePersonByRole(Role role)
        {
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            string email = tbxEmail.Text;
            string password = tbxPassword.Text;
            IPerson person = null;

            switch (role)
            {
                case Role.Admin:
                    person = new Admin(firstName, lastName, email, password, role);
                    break;
                case Role.Mentor:
                    person = new Mentor(firstName, lastName, email, password, role);
                    break;
                default:
                    person = new Mentee(firstName, lastName, email, password, role);
                    break;
            }

            return person;
        }

        private void AddPersonToUserManager(IPerson person)
        {
            try
            {
                userManager.AddPerson(person);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            IPerson selectedPerson = lbxUsers.SelectedItem as IPerson;

            if (selectedPerson == null)
            {
                MessageBox.Show("No person selected to remove.");
                return;
            }
            else if (selectedPerson.Email == loggedAdmin.Email)
            {
                MessageBox.Show("Cannot delete self while using the app.");
                return;
            }

            lbxUsers.Items.Remove(selectedPerson);

            try
            {
                userManager.RemovePerson(selectedPerson);
                MessageBox.Show("User successfully removed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}