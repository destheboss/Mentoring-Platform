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
            bool[] checkResults = userManager.CheckCredentialsForUser(Email, Password);

            if (checkResults[0] == true && checkResults[1] == true)
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
