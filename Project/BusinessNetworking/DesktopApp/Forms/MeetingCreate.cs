﻿using BusinessLogicLayer.Managers;
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
    public partial class MeetingCreate : Form
    {
        private UserManager userManager;
        private MeetingManager meetingManager;
        private Meetings meetingsForm;
        public MeetingCreate(UserManager userManager, MeetingManager meetingManager, Meetings meetingsForm)
        {
            this.userManager = userManager;
            this.meetingManager = meetingManager;
            this.meetingsForm = meetingsForm;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string mentorEmail = tbxMentorEmail.Text;
            Mentor mentor = userManager.GetPersonByEmail(mentorEmail) as Mentor;

            string menteeEmail = tbxMenteeEmail.Text;
            Mentee mentee = userManager.GetPersonByEmail(menteeEmail) as Mentee;

            if (mentor == null)
            {
                MessageBox.Show("Mentor email not found in the database.");
                return; // Exit the method if mentor not found
            }

            if (mentee == null)
            {
                MessageBox.Show("Mentee email not found in the database.");
                return; // Exit the method if mentee not found
            }

            DateTime date = dtpDate.Value;
            Meeting meeting = new Meeting(date, mentor.Id, mentorEmail, mentee.Id, menteeEmail);

            try
            {
                meetingManager.AddMeeting(meeting);
                MessageBox.Show("Meeting successfully created.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating meeting: " + ex.Message);
            }
        }
    }
}