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
    public partial class AnnouncementEdit : Form
    {
        private AnnouncementManager announcementManager;
        private Announcements announcementsForm;
        private Announcement announcement;
        private IPerson loggedAdmin;
        public AnnouncementEdit(AnnouncementManager announcementManager, Announcements announcementsForm, Announcement announcement, IPerson loggedAdmin)
        {
            this.announcementManager = announcementManager;
            this.announcementsForm = announcementsForm;
            this.announcement = announcement;
            this.loggedAdmin = loggedAdmin;
            InitializeComponent();
        }

        private void AnnouncementEdit_Load(object sender, EventArgs e)
        {
            lblTitle.Text = $"Title: {announcement.Title}";
            lblType.Text = $"Type: {announcement.Type}";
            lblCreatedBy.Text = $"Created by: {announcement.CreatedBy}";
            tbMessage.Text = announcement.Message.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string message = tbMessage.Text;

            try
            {
                announcementManager.UpdateAnnouncement(announcement.Id, message);
                MessageBox.Show("Successfully updated.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}