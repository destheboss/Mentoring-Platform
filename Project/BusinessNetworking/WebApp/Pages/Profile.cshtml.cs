using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;
        public User ProfileUser { get; private set; }
        public string CurrentUserEmail { get; private set; }
        public bool IsCurrentUserProfile { get; private set; }

        public ProfileModel(IPersonDataAccess personDataAccess)
        {
            _personDataAccess = personDataAccess;
        }
        public IActionResult OnGet(string email)
        {
            CurrentUserEmail = HttpContext.User.Identity.Name;
            IsCurrentUserProfile = string.Equals(CurrentUserEmail, email, StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(email))
            {
                // If no specific email is provided, load the profile of the logged-in user.
                ProfileUser = _personDataAccess.GetPersonByEmail(CurrentUserEmail) as User;
            }
            else
            {
                // Load the profile of the specified email.
                ProfileUser = _personDataAccess.GetPersonByEmail(email) as User;
            }

            if (ProfileUser == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
