using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        private readonly UserManager userManager;

        public LoginModel(UserManager userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string message = string.Empty;

            if (userManager.CheckCredentialsForUser(Email, Password))
            {
                //HttpContext.Session.SetString("email", person.Email);

                message = "Successfully logged in!";
                ViewData["Message"] = message;

                RedirectToPage("Index");
            }
            else
            {
                message = "Wrong credentials.";
                ViewData["Message"] = message;
            }

            return null;
        }
    }
}
