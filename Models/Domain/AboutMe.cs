using Microsoft.AspNetCore.Identity;

namespace Capstone_23_Proteine.Models.Domain
{
    public class AboutMe
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DietaryOptions { get; set; }
        public string UserActivity { get; set; }
        public string UserId { get; set; } // Foreign key to IdentityUser

        // Navigation property to the associated IdentityUser
        public IdentityUser User { get; set; }

    }
}