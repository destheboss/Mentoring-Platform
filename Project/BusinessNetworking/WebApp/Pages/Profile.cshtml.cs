using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;
        public User User { get; private set; }

        public ProfileModel(IPersonDataAccess personDataAccess)
        {
            _personDataAccess = personDataAccess;
        }
        public void OnGet()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            if (!string.IsNullOrEmpty(userEmail))
            {
                IPerson person = _personDataAccess.GetPersonByEmail(userEmail);

                if (person != null && person is User user)
                {
                    User = user;
                }
                else
                {
                    // Handle case where user is not found or is not a User type
                    // Redirect to login page or show an error message
                }
            }
            else
            {
                // Redirect to login page or show an error message if email not found in session
            }
        }
    }
}
