using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        private readonly AuthenticationManager authenticationManager;

        public LoginModel(AuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool isAuthenticated = authenticationManager.CheckCredentialsForUser(Email, Password);
            string message = string.Empty;

            if (isAuthenticated)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, Email) };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                HttpContext.Session.SetString("UserEmail", Email);

                message = "Successfully logged in!";
                ViewData["Message"] = message;

                return RedirectToPage("/HomeLoggedIn");
            }
            else
            {
                message = "Wrong credentials.";
                ViewData["Message"] = message;
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
}