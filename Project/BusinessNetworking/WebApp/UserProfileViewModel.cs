using BusinessLogicLayer.Models;

namespace WebApp
{
    public class UserProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // Add Image, Specialties etc.
        public List<Specialty> Specialties { get; set; }
    }
}
