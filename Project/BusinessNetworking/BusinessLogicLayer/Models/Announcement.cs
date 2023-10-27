using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public StringBuilder Message { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AnnouncementType Type { get; set; }

        public Announcement()
        {
           this. Message = new StringBuilder();
        }

        public Announcement(string title, string initialMessage, string createdBy, AnnouncementType eventType) : this()
        {
            this.Title = title;
            this.Message.Append(initialMessage);
            this.CreatedBy = createdBy;
            this.CreatedAt = DateTime.Now;
            this.Type = eventType;
        }

        public override string ToString()
        {
            return $"[{CreatedAt}] {Title} - {Type}";
        }
    }
}