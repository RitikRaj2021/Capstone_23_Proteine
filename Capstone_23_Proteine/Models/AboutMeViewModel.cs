using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Capstone_23_Proteine.Models
{
    public class AboutMeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DietaryOptions { get; set; }
        public string UserActivity { get; set; }
    }
}
