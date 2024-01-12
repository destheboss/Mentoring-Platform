using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class MentorProfileModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;

        public User User { get; private set; }
        public string CurrentUserEmail { get; private set; }
        public Mentor Mentor { get; private set; }
        public Mentor ProfileOwner { get; private set; }
        public List<Specialty> Specialties { get; private set; }

        public MentorProfileModel(IPersonDataAccess personDataAccess)
        {
            _personDataAccess = personDataAccess;
        }

        public IActionResult OnGet(string email)
        {
            CurrentUserEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Login");
            }

            Mentor profileOwner = _personDataAccess.GetPersonByEmail(email) as Mentor;

            if (profileOwner != null)
            {
                ProfileOwner = profileOwner;
                Specialties = _personDataAccess.GetMentorSpecialties(profileOwner.Id).ToList();
                User = HttpContext.Session.GetString("UserEmail") != null ? _personDataAccess.GetPersonByEmail(HttpContext.Session.GetString("UserEmail")) as User : null;
                return Page();
            }

            return RedirectToPage("/Error");
        }
    }
}