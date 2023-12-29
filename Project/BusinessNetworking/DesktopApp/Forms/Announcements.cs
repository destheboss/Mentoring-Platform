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
    public partial class Announcements : Form
    {
        private IPerson loggedAdmin;
        private AnnouncementManager announcementManager;
        private Main mainForm;
        public Announcements(IPerson admin, AnnouncementManager announcementManager, Main mainForm)
        {
            this.loggedAdmin = admin;
            this.announcementManager = announcementManager;
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void Announcements_Load(object sender, EventArgs e)
        {
            this.WindowState = mainForm.WindowState;

            foreach (AnnouncementType type in Enum.GetValues(typeof(AnnouncementType)))
            {
                cbxType.Items.Add(type);
            }

            foreach (var announcement in announcementManager.GetAllAnnouncements())
            {
                lbxAnnouncements.Items.Add(announcement);
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string title = tbTitle.Text;
            string message = tbMessage.Text;
            AnnouncementType type = (AnnouncementType)cbxType.SelectedItem;
            string createdBy = loggedAdmin.FirstName + " " + loggedAdmin.LastName;

            try
            {
                Announcement announcement = new Announcement(title, message, createdBy, type);
                announcementManager.CreateAnnouncement(announcement);
                MessageBox.Show("Announcement successfully created!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Announcement announcement = lbxAnnouncements.SelectedItem as Announcement;

            if (lbxAnnouncements.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an announcement to read its message.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"{announcement.Message}");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Announcement announcement = lbxAnnouncements.SelectedItem as Announcement;

            if (lbxAnnouncements.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid item to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            announcementManager.DeleteAnnouncement(announcement.Id);
            lbxAnnouncements.Items.Remove(announcement);
            MessageBox.Show("Announcement successfully deleted!");
        }

        private void rbNewest_CheckedChanged(object sender, EventArgs e)
        {
            var sortedList = announcementManager.GetAllAnnouncements();

            sortedList.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            lbxAnnouncements.Items.Clear();

            foreach (var announcement in sortedList)
            {
                lbxAnnouncements.Items.Add(announcement);
            }
        }

        private void rbOldest_CheckedChanged(object sender, EventArgs e)
        {
            var sortedList = announcementManager.GetAllAnnouncements();

            sortedList.Sort((x, y) => x.CreatedAt.CompareTo(y.CreatedAt));
            lbxAnnouncements.Items.Clear();

            foreach (var announcement in sortedList)
            {
                lbxAnnouncements.Items.Add(announcement);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbxAnnouncements.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid item to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Announcement announcement = lbxAnnouncements.SelectedItem as Announcement;
            AnnouncementEdit form = new AnnouncementEdit(announcementManager, this, announcement, loggedAdmin);
            form.ShowDialog();
        }
    }
}