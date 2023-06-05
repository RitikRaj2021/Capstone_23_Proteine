using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Models;
using Capstone_23_Proteine.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Capstone_23_Proteine.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public AboutMeController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        // GET: /AboutMe
        [HttpGet]
        public IActionResult AboutMe()
        {
            return View();
        }

        // POST: /AboutMe
        [HttpPost]
        public async Task<IActionResult> AboutMe(AboutMeViewModel aboutMeRequest)
        {

            ViewData["FirstName"] = aboutMeRequest.FirstName;
            ViewData["LastName"] = aboutMeRequest.LastName;
            ViewData["Gender"] = aboutMeRequest.Gender;
            ViewData["Height"] = aboutMeRequest.Height;
            ViewData["Weight"] = aboutMeRequest.Weight;
            ViewData["DateOfBirth"] = aboutMeRequest.DateOfBirth;
            ViewData["DietaryOptions"] = aboutMeRequest.DietaryOptions;
            ViewData["UserActivity"] = aboutMeRequest.UserActivity;

            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

            // Retrieve the existing AboutMe entry for the user, if it exists
            var existingAboutMe = applicationDbContext.AboutMe.FirstOrDefault(a => a.UserId == userId);

            if (existingAboutMe != null)
            {
                // Delete the existing entry
                applicationDbContext.AboutMe.Remove(existingAboutMe);
            }

            // Create a new AboutMe object with the user-provided data
            var aboutme = new AboutMe()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FirstName = aboutMeRequest.FirstName,
                LastName = aboutMeRequest.LastName,
                Gender = aboutMeRequest.Gender,
                Height = aboutMeRequest.Height,
                Weight = aboutMeRequest.Weight,
                DateOfBirth = aboutMeRequest.DateOfBirth,
                DietaryOptions = aboutMeRequest.DietaryOptions.ToString(),
                UserActivity = aboutMeRequest.UserActivity,
            };

            // Add the AboutMe object to the AboutMe DbSet and save changes to the database
            await applicationDbContext.AboutMe.AddAsync(aboutme);
            await applicationDbContext.SaveChangesAsync();

            // Redirect to the AboutMe action to display the updated AboutMe page
            return RedirectToAction("AboutMe");
        }
    }
}
