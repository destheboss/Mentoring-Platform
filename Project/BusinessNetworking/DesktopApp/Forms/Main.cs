using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Models;
using DataAccessLayer.Managers;

namespace DesktopApp.Forms
{
    public partial class Main : Form
    {
        private IPerson loggedAdmin;
        private IAnnouncementDataAccess announcementData = new AnnouncementDataManager();
        private AnnouncementManager announcementManager;
        private UserManager userManager;
        private MeetingManager meetingManager;
        public Main(IPerson admin, UserManager userManager, MeetingManager meetingManager)
        {
            this.loggedAdmin = admin;
            this.userManager = userManager;
            this.meetingManager = meetingManager;
            this.announcementManager = new AnnouncementManager(announcementData);

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblInfo.Text = $"Welcome {loggedAdmin.FirstName}, to Admin Panel";

            lbxAnnouncements.Items.Clear();

            foreach (var announcement in announcementManager.GetAllAnnouncements())
            {
                lbxAnnouncements.Items.Add(announcement);
            }
        }

        private void btnMeetings_Click(object sender, EventArgs e)
        {
            Meetings form = new Meetings(loggedAdmin, userManager, meetingManager, this);
            this.Hide();
            form.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Users form = new Users(loggedAdmin, meetingManager, userManager, this);
            this.Hide();
            form.ShowDialog();
        }

        private void btnAnnouncements_Click(object sender, EventArgs e)
        {
            Announcements form = new Announcements(loggedAdmin, announcementManager, this);
            this.Hide();
            form.ShowDialog();
        }
    }
}