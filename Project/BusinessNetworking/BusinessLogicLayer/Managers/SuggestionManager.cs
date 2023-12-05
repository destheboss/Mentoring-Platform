using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class SuggestionManager : ISuggestionManager
    {
        private readonly IMeetingDataAccess meetingDataAccess;
        private readonly IPersonDataAccess personDataAccess;

        public SuggestionManager(IMeetingDataAccess meetingDataAccess, IPersonDataAccess personDataAccess)
        {
            this.meetingDataAccess = meetingDataAccess;
            this.personDataAccess = personDataAccess;
        }

        // Main method for suggesting mentors to a mentee based on various criteria
        public IEnumerable<Mentor> SuggestMentorsForMentee(string menteeEmail)
        {
            // Retrieve all meetings for the mentee and calculate their preferred specialties
            var menteeMeetings = meetingDataAccess.GetAllMeetings(menteeEmail);
            var preferredSpecialties = GetPreferredSpecialties(menteeMeetings);

            // Retrieve all potential mentors and calculate a compatibility score for each
            var potentialMentors = personDataAccess.GetMentors();

            return potentialMentors
                .Select(mentor => new
                {
                    Mentor = mentor,
                    Score = CalculateCompatibilityScore(mentor, preferredSpecialties, menteeMeetings)
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Mentor);
        }

        // Analyze the mentee's past meetings to determine preferred specialties
        private IEnumerable<Specialty> GetPreferredSpecialties(IEnumerable<Meeting> menteeMeetings)
        {
            var specialtyScores = new Dictionary<Specialty, double>();
            var mostRecentMeetingDate = menteeMeetings.Max(m => m.DateTime);

            foreach (var meeting in menteeMeetings)
            {
                // Apply time decay factor to give more weight to recent meetings
                var timeDecayFactor = CalculateTimeDecayFactor(meeting.DateTime, mostRecentMeetingDate);
                var mentorSpecialties = personDataAccess.GetMentorSpecialties(meeting.MentorId);

                foreach (var specialty in mentorSpecialties)
                {
                    if (!specialtyScores.ContainsKey(specialty))
                        specialtyScores[specialty] = 0;

                    specialtyScores[specialty] += timeDecayFactor;
                }
            }

            // Return the top specialties based on the calculated scores
            return specialtyScores.OrderByDescending(kv => kv.Value).Take(3).Select(kv => kv.Key);
        }

        // Calculate a decay factor based on how much time has passed since the meeting
        private double CalculateTimeDecayFactor(DateTime meetingDate, DateTime mostRecentMeetingDate)
        {
            var daysSinceMeeting = (mostRecentMeetingDate - meetingDate).TotalDays;
            return 1 / (1 + daysSinceMeeting); // A simple decay function
        }

        // Calculate a combined score for mentor compatibility based on specialty, performance, and recent activity
        private double CalculateCompatibilityScore(Mentor mentor, IEnumerable<Specialty> preferredSpecialties, IEnumerable<Meeting> menteeMeetings)
        {
            double specialtyScore = CalculateSpecialtyScore(mentor, preferredSpecialties);
            double performanceScore = CalculatePerformanceScore(mentor, menteeMeetings);
            double availabilityScore = CalculateRecentActivityScore(mentor);

            // Example weights: 40% for specialty, 40% for performance, 20% for availability
            return specialtyScore * 0.4 + performanceScore * 0.4 + availabilityScore * 0.2;
        }

        // Calculate how well the mentor's specialties align with the mentee's preferences
        private double CalculateSpecialtyScore(Mentor mentor, IEnumerable<Specialty> preferredSpecialties) 
        {
            int matchCount = mentor.Specialties.Count(s => preferredSpecialties.Contains(s));
            return matchCount / (double)mentor.Specialties.Count;
        }

        // Calculate a score based on the mentor's average meeting rating
        private double CalculatePerformanceScore(Mentor mentor, IEnumerable<Meeting> menteeMeetings) 
        {
            var mentorMeetings = meetingDataAccess.GetAllMeetings(mentor.Email);
            if (!mentorMeetings.Any())
                return 0;

            double averageRating = mentorMeetings.Average(m => m.Rating);
            return averageRating / 5.0;
        }

        // Calculate a score based on the mentor's recent meeting frequency
        private double CalculateRecentActivityScore(Mentor mentor)
        {
            const int recentPeriodInDays = 21;
            DateTime threeWeeksAgo = DateTime.Now.AddDays(-recentPeriodInDays);

            var recentMeetings = meetingDataAccess.GetPastMeetings(mentor.Email)
                                                  .Where(m => m.DateTime >= threeWeeksAgo)
                                                  .ToList();

            // Group meetings by day
            var meetingsByDay = recentMeetings.GroupBy(m => m.DateTime.Date)
                                              .ToDictionary(g => g.Key, g => g.Count());

            // Calculate the average number of meetings per day
            double averageMeetingsPerDay = meetingsByDay.Values.Any()
                                           ? meetingsByDay.Values.Average()
                                           : 0;

            return averageMeetingsPerDay; // Higher frequency indicates lower availability
        }
    }
}