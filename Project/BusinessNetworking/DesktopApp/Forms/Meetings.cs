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
    public partial class Meetings : Form
    {
        private IPerson loggedAdmin;
        private UserManager userManager;
        private MeetingManager meetingManager;
        private Main mainForm;
        public Meetings(IPerson admin, UserManager userManager, MeetingManager meetingManager, Main mainForm)
        {
            this.loggedAdmin = admin;
            this.userManager = userManager;
            this.meetingManager = meetingManager;
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
            this.WindowState = mainForm.WindowState;

            lbxMeetings.Items.Clear();

            foreach (var meeting in meetingManager.GetAllMeetings("jon@gmail.com"))
            {
                lbxMeetings.Items.Add(meeting);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchAndDisplayMeetings();
        }

        private void SearchAndDisplayMeetings()
        {
            string email = tbxEmail.Text;
            IEnumerable<Meeting> filteredMeetings;

            if (rbAllMeetings.Checked)
            {
                filteredMeetings = meetingManager.GetAllMeetings(email);
            }
            else if (rbPastMeetings.Checked)
            {
                filteredMeetings = meetingManager.GetPastMeetings(email);
            }
            else
            {
                filteredMeetings = meetingManager.GetUpcomingMeetings(email);
            }
            lbxMeetings.Items.Clear();

            foreach (var meeting in filteredMeetings)
            {
                lbxMeetings.Items.Add(meeting);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbxMeetings.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a meeting to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Meeting selectedMeeting = (Meeting)lbxMeetings.SelectedItem;
            MeetingEdit form = new MeetingEdit(userManager, meetingManager, this, selectedMeeting);
            form.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbxMeetings.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a meeting to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Meeting selectedMeeting = (Meeting)lbxMeetings.SelectedItem;
                meetingManager.CancelMeeting(selectedMeeting);
                lbxMeetings.Items.Remove(selectedMeeting);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            MeetingCreate form = new MeetingCreate(userManager, meetingManager, this);
            form.ShowDialog();
        }

        private void rbAllMeetings_CheckedChanged(object sender, EventArgs e)
        {
            SearchAndDisplayMeetings();
        }

        private void rbUpcomingMeetings_CheckedChanged(object sender, EventArgs e)
        {
            SearchAndDisplayMeetings();
        }

        private void rbPastMeetings_CheckedChanged(object sender, EventArgs e)
        {
            SearchAndDisplayMeetings();
        }
    }
}