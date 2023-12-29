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

        private const double SpecialtyWeight = 0.4;
        private const double PerformanceWeight = 0.4;
        private const double AvailabilityWeight = 0.2;
        private const double MaxRating = 5.0;
        private const int recentPeriodInDays = 21;
        private const int minimumMatchCount = 1;

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

            if (!preferredSpecialties.Any()) // Check if there are no preferred specialties
            {
                // Handle this scenario - e.g., return all mentors or a default set of mentors
                return personDataAccess.GetMentors();
            }

            // Retrieve all potential mentors and calculate a compatibility score for each
            var potentialMentors = personDataAccess.GetMentors();

            return potentialMentors
                .Select(mentor => new
                {
                    Mentor = mentor,
                    Score = CalculateCompatibilityScore(mentor, preferredSpecialties, menteeMeetings)
                })
                .Where(x => x.Score > 0 && DoesMeetMinimumSpecialtyRequirement(x.Mentor, preferredSpecialties))
                .OrderByDescending(x => x.Score)
                .Select(x => x.Mentor);
        }

        private bool DoesMeetMinimumSpecialtyRequirement(Mentor mentor, IEnumerable<Specialty> preferredSpecialties)
        {
            return mentor.Specialties.Count(s => preferredSpecialties.Contains(s)) >= minimumMatchCount;
        }

        // Analyze the mentee's past meetings to determine preferred specialties
        private IEnumerable<Specialty> GetPreferredSpecialties(IEnumerable<Meeting> menteeMeetings)
        {
            if (!menteeMeetings.Any()) // Check if there are no meetings
            {
                return Enumerable.Empty<Specialty>(); // Return an empty list of specialties
            }

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
            double performanceScore = menteeMeetings.Any() ? CalculatePerformanceScore(mentor, menteeMeetings) : 0;
            double availabilityScore = menteeMeetings.Any() ? CalculateRecentActivityScore(mentor) : 0;

            // Example weights: 40% for specialty, 40% for performance, 20% for availability
            return specialtyScore * SpecialtyWeight + performanceScore * PerformanceWeight + availabilityScore * AvailabilityWeight;
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
            return averageRating / MaxRating;
        }

        // Calculate a score based on the mentor's recent meeting frequency
        private double CalculateRecentActivityScore(Mentor mentor)
        {
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