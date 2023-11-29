using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Factories;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager userManager;
        private readonly IWebHostEnvironment environment;

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public Role Role { get; set; }

        [BindProperty]
        public List<Specialty> Specialties { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public RegisterModel(UserManager userManager, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.environment = environment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return Page();
            }

            string filePath = null;
            if (Image != null)
            {
                string uploadsFolder = Path.Combine(environment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Image.FileName);
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                filePath = "/images/" + uniqueFileName;
            }

            try
            {
                User newUser = UserFactory.CreateUser(FirstName, LastName, Email, Password, Role, Specialties, filePath);
                userManager.AddPerson(newUser);

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while creating the account: {ex.Message}");
                return Page();
            }
        }
    }
}