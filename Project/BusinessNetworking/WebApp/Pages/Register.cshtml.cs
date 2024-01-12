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

        public string SpecialtiesString { get; set; }

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
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        // Log these error messages or set breakpoints to examine them.
                    }
                }
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
                List<Specialty> selectedSpecialties = new List<Specialty>();
                if (Role == Role.Mentor)
                {
                    SpecialtiesString = Request.Form["SpecialtiesString"];

                    if (string.IsNullOrEmpty(SpecialtiesString))
                    {
                        ModelState.AddModelError("SpecialtiesString", "Specialties are required for a Mentor.");
                        return Page();
                    }

                    selectedSpecialties = SpecialtiesString
                    .Split(',')
                    .Select(s => (Specialty)Enum.Parse(typeof(Specialty), s))
                    .ToList();
                }

                User newUser = UserFactory.CreateUser(FirstName, LastName, Email, Password, Role, selectedSpecialties, filePath);
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