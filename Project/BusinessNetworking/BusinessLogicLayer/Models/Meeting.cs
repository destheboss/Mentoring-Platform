using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Meeting
    {
        private int id;
        private DateTime dateTime;
        private int mentorId;
        private string mentorEmail;
        private int menteeId;
        private string menteeEmail;
        private int rating;

        public int Id { get; set; }
        public DateTime DateTime { get => this.dateTime; set => this.dateTime = value; }
        public int MentorId { get => this.mentorId; set => this.mentorId = value; }
        public string MentorEmail { get => this.mentorEmail; set => this.mentorEmail = value;}
        public int MenteeId { get => this.menteeId; set => this.menteeId = value; }
        public string MenteeEmail { get => this.menteeEmail; set => this.menteeEmail = value;}
        public int Rating { get => this.rating; set => this.rating = value;}

        // Creation of meeting
        public Meeting(DateTime dateTime, int mentorId, string mentorEmail, int menteeId, string menteeEmail)
        {
            this.DateTime = dateTime;
            this.mentorId = mentorId;
            this.MentorEmail = mentorEmail;
            this.menteeId = menteeId;
            this.MenteeEmail = menteeEmail;
            this.Rating = 0;
        }

        // Pulling meeting data from database
        public Meeting(int id, DateTime dateTime, int mentorId, string mentorEmail, int menteeId, string menteeEmail, int rating)
        {
            this.Id = id;
            this.DateTime = dateTime;
            this.MentorId = mentorId;
            this.MentorEmail = mentorEmail;
            this.MenteeId = menteeId;
            this.MenteeEmail = menteeEmail;
            this.Rating = rating;
        }

        public override string ToString()
        {
            return $"Participants: {MentorEmail}, {MenteeEmail}, Date: {this.DateTime}, Rating: {this.Rating}";
        }
    }
}