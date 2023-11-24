using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Models;
using DataAccessLayer.Managers;
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
        private SearchManager searchManager;
        private UserActionsManager userActionsManager;
        private RatingManager ratingManager;
        private IUserActionsDataAccess data = new UserActionsDataManager();
        private Role? selectedRole = null;
        public Users(IPerson admin, MeetingManager meetingManager, UserManager userManager, Main mainForm)
        {
            this.loggedAdmin = admin;
            this.meetingManager = meetingManager;
            this.userManager = userManager;
            this.mainForm = mainForm;
            this.searchManager = new SearchManager(userManager, meetingManager);
            this.userActionsManager = new UserActionsManager(data);
            this.ratingManager = new RatingManager(userManager, meetingManager);
            InitializeComponent();
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

            List<object> searchItems = new List<object> { "All roles" };
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                searchItems.Add((object)role);
            }
            cbxSearch.DataSource = searchItems;

            lbxUsers.Items.Clear();

            foreach (var person in userManager.GetAllPersons())
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

        private User CreatePersonByRole(Role role)
        {
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            string email = tbxEmail.Text;
            string password = tbxPassword.Text;
            User person = null;

            switch (role)
            {
                case Role.Admin:
                    person = new Admin(firstName, lastName, email, password, role);
                    break;
                case Role.Mentor:
                    person = new Mentor(firstName, lastName, email, password, role);
                    break;
                case Role.Mentee:
                    person = new Mentee(firstName, lastName, email, password, role);
                    break;
                default:
                    break;
            }

            return person;
        }

        private void AddPersonToUserManager(User user)
        {
            try
            {
                userManager.AddPerson(user);
                MessageBox.Show("User successfully created!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSuspendPerson_Click(object sender, EventArgs e)
        {
            IPerson selectedPerson = lbxUsers.SelectedItem as IPerson;

            if (selectedPerson == null)
            {
                MessageBox.Show("No person selected to suspend.");
                return;
            }

            try
            {
                if (selectedPerson is User user)
                {
                    userActionsManager.SuspendUser(user);
                    MessageBox.Show("User successfully suspended.");
                    UpdateUserStatusInListBox(user);
                }
                else
                {
                    MessageBox.Show("The selected person cannot be suspended because it is not a User.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            object selectedItem = cbxSearch.SelectedItem;

            if (selectedItem is string && (string)selectedItem == "All roles")
            {
                selectedRole = null;
            }
            else if (selectedItem is Role)
            {
                selectedRole = (Role)selectedItem;
            }
            UpdateUserList();
        }

        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cbxSearch.SelectedItem;
            if (selectedItem is Role role)
            {
                selectedRole = role;
            }
            else if (selectedItem is string str && str == "All roles")
            {
                selectedRole = null;
            }
            UpdateUserList();
        }

        private void UpdateUserList()
        {
            var query = tbxSearch.Text;
            var filteredUsers = searchManager.SearchByName(query);

            lbxUsers.Items.Clear();

            foreach (var user in filteredUsers)
            {
                if (selectedRole == null)
                {
                    lbxUsers.Items.Add(user);
                }
                else
                {
                    if (user.Role.Equals(selectedRole))
                    {
                        lbxUsers.Items.Add(user);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbxUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a user to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User selectedUser = (User)lbxUsers.SelectedItem;
            UsersEdit form = new UsersEdit(userManager, this, selectedUser);
            form.ShowDialog();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            IPerson selectedPerson = lbxUsers.SelectedItem as IPerson;

            if (selectedPerson == null)
            {
                MessageBox.Show("No person selected to activate.");
                return;
            }

            try
            {
                if (selectedPerson is User user)
                {
                    userActionsManager.UnsuspendUser(user);
                    MessageBox.Show("User successfully activated.");
                    UpdateUserStatusInListBox(user);
                }
                else
                {
                    MessageBox.Show("The selected person cannot be activated because it is not a User.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateUserStatusInListBox(IPerson updatedPerson)
        {
            lbxUsers.DataSource = null;
            lbxUsers.DataSource = userManager.GetAllPersons();
            lbxUsers.DisplayMember = "DisplayMember";

            int index = lbxUsers.Items.IndexOf(updatedPerson);
            if (index != -1)
            {
                lbxUsers.Items[index] = updatedPerson;
                lbxUsers.Refresh();
            }

            lbxUsers.SelectedIndex = index;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            IPerson selectedPerson = lbxUsers.SelectedItem as IPerson;

            if (selectedPerson == null)
            {
                MessageBox.Show("No person selected to delete.");
                return;
            }

            try
            {
                if (selectedPerson is Admin admin)
                {
                    MessageBox.Show("The selected person cannot be removed because it is an Admin.");
                }
                else
                {
                    userManager.RemovePerson(selectedPerson as User);
                    MessageBox.Show("User successfully removed.");
                    lbxUsers.Items.Remove(selectedPerson);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}