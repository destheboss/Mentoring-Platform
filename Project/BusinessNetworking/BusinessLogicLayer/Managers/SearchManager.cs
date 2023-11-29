using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Managers
{
    public class SearchManager
    {
        private readonly UserManager personData;
        private readonly MeetingManager meetingData;
        private Dictionary<Specialty, List<Mentor>> specialtyMentorMap;

        public SearchManager(UserManager personData, MeetingManager meetingData)
        {
            this.personData = personData;
            this.meetingData = meetingData;
            specialtyMentorMap = new Dictionary<Specialty, List<Mentor>>();
        }

        public List<IPerson> SearchByName(string query)
        {
            query = query.ToLower().Trim();
            Regex regex = new Regex(Regex.Escape(query), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            List<IPerson> filteredPersons = new List<IPerson>();
            var allPersons = personData.GetAllPersons();

            foreach (var person in allPersons)
            {
                string combinedName = $"{person.FirstName} {person.LastName}".ToLower();
                if (regex.IsMatch(person.FirstName) || regex.IsMatch(person.LastName) || combinedName.Contains(query))
                {
                    filteredPersons.Add(person);
                }
            }
            return filteredPersons;
        }

        public List<Mentor> GetMentorsBySpecialty(Specialty specialty)
        {
            if (specialtyMentorMap.ContainsKey(specialty))
            {
                return specialtyMentorMap[specialty];
            }

            List<Mentor> mentors = personData.GetMentors()
                .Where(m => m.Specialties.Contains(specialty))
                .ToList();
            specialtyMentorMap[specialty] = mentors;
            return mentors;
        }
    }
}