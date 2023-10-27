using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
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
        private MeetingManager meetingManager;
        private Main mainForm;
        public Meetings(IPerson admin, MeetingManager meetingManager, Main mainForm)
        {
            this.loggedAdmin = admin;
            this.meetingManager = meetingManager;
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.Show();
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
            this.WindowState = mainForm.WindowState;
        }
    }
}