using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class AnnouncementManager
    {
        private readonly IAnnouncementDataAccess data;

        public AnnouncementManager(IAnnouncementDataAccess data)
        {
            this.data = data;
        }

        public bool CreateAnnouncement(Announcement announcement)
        {
            return data.CreateAnnouncement(announcement);
        }

        public bool DeleteAnnouncement(int id)
        {
            return data.DeleteAnnouncement(id);
        }

        public List<Announcement> GetAllAnnouncements()
        {
            return data.GetAllAnnouncements();
        }

        public bool UpdateAnnouncement(int id, string newContent)
        {
            return data.UpdateAnnouncement(id, newContent);
        }
    }
}