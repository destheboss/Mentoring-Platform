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
            dgvAnnouncements.Rows.Clear();

            foreach (var announcement in announcementManager.GetAllAnnouncements())
            {
                int rowIndex = dgvAnnouncements.Rows.Add();
                DataGridViewRow row = dgvAnnouncements.Rows[rowIndex];

                row.Cells["Id"].Value = announcement.Id;
                row.Cells["Title"].Value = announcement.Title;
                row.Cells["CreatedBy"].Value = announcement.CreatedBy;
                row.Cells["CreatedAt"].Value = announcement.CreatedAt;
                row.Cells["UpdatedAt"].Value = announcement.UpdatedAt;
                row.Cells["Type"].Value = announcement.Type.ToString();
            }

            foreach (DataGridViewColumn column in dgvAnnouncements.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }
    }
}