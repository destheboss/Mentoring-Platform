using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class SearchManager
    {
        private readonly UserManager personData;
        private readonly MeetingManager meetingData;

        public SearchManager(UserManager personData, MeetingManager meetingData)
        {
            this.personData = personData;
            this.meetingData = meetingData;
        }

        public List<IPerson> SearchByName(string query)
        {
            List<IPerson> filteredPersons = new List<IPerson>();
            var allPersons = personData.GetAllPersons();

            foreach (var person in allPersons)
            {
                if (person.FirstName.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                {
                    filteredPersons.Add(person);
                }
            }
            return filteredPersons;
        }

        public List<Mentor> FilterByRating()
        {
            List<Mentor> mentors = personData.GetMentors().ToList();
            mentors.Sort((x, y) => y.Rating.CompareTo(x.Rating));
            return mentors;
        }
    }
}