using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BusinessLogicLayer.Managers;

namespace WebApp.Pages
{
    public class BrowseMentorsModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;
        private readonly ISuggestionManager _suggestionManager;

        public IEnumerable<Mentor> Mentors { get; set; }

        public BrowseMentorsModel(IPersonDataAccess personDataAccess, ISuggestionManager suggestionManager)
        {
            _personDataAccess = personDataAccess;
            _suggestionManager = suggestionManager;
        }

        public void OnGet(bool suggest = false)
        {
            if (suggest && User.Identity.IsAuthenticated)
            {
                string userEmail = User.Identity.Name;
                Mentors = _suggestionManager.SuggestMentorsForMentee(userEmail);
            }
            else
            {
                Mentors = _personDataAccess.GetMentors();
            }
        }
    }
}