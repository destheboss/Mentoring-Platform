using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BookMeetingModel : PageModel
    {
        private readonly IMeetingDataAccess _meetingDataAccess;
        private readonly IPersonDataAccess _personDataAccess;

        public BookMeetingModel(IMeetingDataAccess meetingDataAccess, IPersonDataAccess personDataAccess)
        {
            _meetingDataAccess = meetingDataAccess;
            _personDataAccess = personDataAccess;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }
        [BindProperty]
        public string MeetingDate { get; set; }

        [BindProperty]
        public string MeetingTime { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public IActionResult OnGet(string mentorEmail)
        {
            // Check if the user is authenticated as a Mentee
            if (!User.Identity.IsAuthenticated)
            {
                // Handle unauthorized access, maybe redirect to a login page or show an error message.
                // For example:
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(mentorEmail))
            {
                ModelState.AddModelError(string.Empty, "Mentor's email is required.");
                return Page();
            }

            Mentor mentor = _personDataAccess.GetPersonByEmail(mentorEmail) as Mentor;

            Mentee mentee = _personDataAccess.GetPersonByEmail(User.Identity.Name) as Mentee;

            if (mentor == null || mentee == null)
            {
                return NotFound();
            }

            Meeting = new Meeting(DateTime.Now, mentor.Id, mentor.Email, mentee.Id, mentee.Email);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Mentor mentor = _personDataAccess.GetPersonByEmail(Meeting.MentorEmail) as Mentor;
            Mentee mentee = _personDataAccess.GetPersonByEmail(Meeting.MenteeEmail) as Mentee;

            if (mentor == null || mentee == null)
            {
                return NotFound();
            }

            if (DateTime.TryParse(MeetingDate + " " + MeetingTime, out DateTime meetingDateTime))
            {
                Meeting.DateTime = meetingDateTime;
                Meeting.MentorId = mentor.Id;
                Meeting.MenteeId = mentee.Id;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid date or time format.");
                return Page();
            }

            _meetingDataAccess.AddMeeting(Meeting);

            SuccessMessage = "Meeting created successfully!";
            ViewData["Message"] = SuccessMessage;
            return Page();
        }
    }
}
