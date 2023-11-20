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
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }
    }
}