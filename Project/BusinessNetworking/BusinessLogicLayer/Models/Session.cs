using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Session
    {
        private DateTime dateTime;
        private string mentorEmail;
        private string menteeEmail;
        private int rating;

        public DateTime DateTime { get => this.dateTime; set => this.dateTime = value; }
        public string MentorEmail { get => this.mentorEmail; set => this.mentorEmail = value;}
        public string MenteeEmail { get => this.menteeEmail; set => this.menteeEmail = value;}
        public int Rating { get => this.rating; set => this.rating = value;}

        public Session(DateTime dateTime, string mentorEmail, string menteeEmail)
        {
            this.DateTime = dateTime;
            this.MentorEmail = mentorEmail;
            this.MenteeEmail = menteeEmail;
            this.Rating = 0;
        }

        public override string ToString()
        {
            return $"Participants: {MentorEmail}, {MenteeEmail}, Date: {this.DateTime}, Rating: {this.Rating}";
        }
    }
}