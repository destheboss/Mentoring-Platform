using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAnnouncementDataAccess
    {
        bool CreateAnnouncement(Announcement announcement);
        bool DeleteAnnouncement(int id);
        List<Announcement> GetAllAnnouncements();
        bool UpdateAnnouncement(int id, string newContent);
        Announcement? GetLatestAnnouncement();
    }
}