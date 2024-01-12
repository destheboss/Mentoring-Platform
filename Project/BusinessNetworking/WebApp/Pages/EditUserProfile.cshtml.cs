using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class EditUserProfileModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;

        [BindProperty]
        public User User { get; set; }

        public EditUserProfileModel(IPersonDataAccess personDataAccess)
        {
            _personDataAccess = personDataAccess;
        }

        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Error");
            }

            User = _personDataAccess.GetPersonByEmail(email) as User;
            if (User == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var currentUser = _personDataAccess.GetPersonByEmail(User.Email) as User;
                if (currentUser == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return Page();
                }

                bool updateResult = _personDataAccess.UpdatePersonInfo(currentUser, User);
                if (!updateResult)
                {
                    ModelState.AddModelError("", "Unable to update user information.");
                    return Page();
                }

                return RedirectToPage("/Profile", new { email = User.Email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
