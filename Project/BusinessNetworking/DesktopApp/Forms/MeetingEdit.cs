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
    public partial class MeetingEdit : Form
    {
        private UserManager userManager;
        private MeetingManager meetingManager;
        private Meetings meetingsForm;
        private Meeting selectedMeeting;
        private RatingManager ratingManager;
        public MeetingEdit(UserManager userManager, MeetingManager meetingManager, Meetings meetingsForm, Meeting selectedMeeting)
        {
            this.userManager = userManager;
            this.meetingManager = meetingManager;
            this.meetingsForm = meetingsForm;
            this.selectedMeeting = selectedMeeting;
            this.ratingManager = new RatingManager(userManager, meetingManager);
            InitializeComponent();
        }

        private void MeetingEdit_Load(object sender, EventArgs e)
        {
            nudRating.Value = selectedMeeting.Rating;
            dtpDate.Value = selectedMeeting.DateTime;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int rating = (int)nudRating.Value;
            DateTime date = dtpDate.Value;

            try
            {
                ratingManager.RateMeeting(selectedMeeting.Id, rating);
                meetingManager.ChangeMeetingTime(selectedMeeting, date);
                MessageBox.Show("Meeting details successfully changed!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}