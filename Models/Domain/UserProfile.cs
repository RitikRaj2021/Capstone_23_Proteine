using Microsoft.AspNetCore.Identity;

namespace Capstone_23_Proteine.Models.Domain
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation property to the associated IdentityUser
        public IdentityUser User { get; set; }
    }
}
