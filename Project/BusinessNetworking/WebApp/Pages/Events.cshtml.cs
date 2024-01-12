using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class EventsModel : PageModel
    {
        private readonly IAnnouncementDataAccess _announcementDataAccess;

        public Announcement LatestAnnouncement { get; set; }

        public EventsModel(IAnnouncementDataAccess announcementDataAccess)
        {
            _announcementDataAccess = announcementDataAccess;
        }

        public void OnGet()
        {
            LatestAnnouncement = _announcementDataAccess.GetLatestAnnouncement();
        }
    }
}
