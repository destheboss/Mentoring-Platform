using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class EditMentorProfileModel : PageModel
    {
        private readonly IPersonDataAccess _personDataAccess;

        [BindProperty]
        public Mentor Mentor { get; set; }
        [BindProperty]
        public List<Specialty> Specialties { get; set; }

        public EditMentorProfileModel(IPersonDataAccess personDataAccess)
        {
            _personDataAccess = personDataAccess;
        }

        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Error");
            }

            Mentor = _personDataAccess.GetPersonByEmail(email) as Mentor;
            Specialties = _personDataAccess.GetMentorSpecialties(Mentor.Id);
            if (Mentor == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return with validation errors
            }

            try
            {
                // Fetch the current mentor information using their email
                var currentMentor = _personDataAccess.GetPersonByEmail(Mentor.Email) as Mentor;

                if (currentMentor == null)
                {
                    ModelState.AddModelError("", "Mentor not found.");
                    return Page();
                }

                // Update mentor information
/*                Mentor.Specialties = SelectedSpecialties; */// Assign the selected specialties to the Mentor object
                bool updateResult = _personDataAccess.UpdatePersonInfo(currentMentor, Mentor);

                if (!updateResult)
                {
                    ModelState.AddModelError("", "Unable to update mentor information.");
                    return Page();
                }

                return RedirectToPage("/MentorProfile", new { email = Mentor.Email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
