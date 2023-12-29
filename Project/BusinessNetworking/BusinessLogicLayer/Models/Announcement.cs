using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Announcement
    {
        private string title;
        private StringBuilder message;
        private string createdBy;
        private AnnouncementType type;

        public int Id { get; set; }
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be null or whitespace.");
                title = value;
            }
        }
        public StringBuilder Message
        {
            get => message;
            set
            {
                if (value == null || (value.Length == 0 && message != null))
                    throw new ArgumentException("Message cannot be null or empty.");
                message = value;
            }
        }
        public string CreatedBy
        {
            get => createdBy;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CreatedBy cannot be null or whitespace.");
                createdBy = value;
            }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AnnouncementType Type
        {
            get => type;
            set
            {
                if (!Enum.IsDefined(typeof(AnnouncementType), value))
                    throw new ArgumentException("Invalid announcement type.");
                type = value;
            }
        }

        public Announcement()
        {
           this. Message = new StringBuilder();
        }

        // Creation of announcement
        public Announcement(string title, string message, string createdBy, AnnouncementType eventType) : this()
        {
            this.Title = title;
            //this.Message.Append(message);
            this.Message = new StringBuilder(message);
            this.CreatedBy = createdBy;
            this.CreatedAt = DateTime.Now;
            this.Type = eventType;
        }

        // Reading from database
        public Announcement(int id, string title, string message, string createdBy, DateTime createdAt, DateTime? updatedAt, AnnouncementType type)
        {
            this.Id = id;
            this.Title = title;
            this.Message = new StringBuilder(message);
            this.CreatedBy = createdBy;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.Type = type;
        }

        public override string ToString()
        {
            return $"[{CreatedAt}] {Type} - {Title}, by: {CreatedBy}";
        }
    }
}