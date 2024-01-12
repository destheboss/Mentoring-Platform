using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class HomeLoggedInModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;
        public User LoggedInUser { get; set; }

        public HomeLoggedInModel(IPersonDataAccess personDataAccess)
        {
            _personDataAccess = personDataAccess;
        }
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userEmail = User.Identity.Name;
                IPerson person = _personDataAccess.GetPersonByEmail(userEmail);
                if (person != null && person is User user)
                {
                    LoggedInUser = user;
                }
            }
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            return RedirectToPage("/Home");
        }
    }
}