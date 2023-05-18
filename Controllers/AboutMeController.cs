using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Models;
using Capstone_23_Proteine.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_23_Proteine.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AboutMeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult AboutMe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AboutMe(AboutMeViewModel aboutMeRequest)
        {
            var aboutme = new AboutMe()
            {
                Id = Guid.NewGuid(),
                FirstName = aboutMeRequest.FirstName,
                LastName = aboutMeRequest.LastName,
                Gender = aboutMeRequest.Gender,
                Height = aboutMeRequest.Height,
                Weight = aboutMeRequest.Weight,
                DateOfBirth = aboutMeRequest.DateOfBirth,
                DietaryOptions = aboutMeRequest.DietaryOptions.ToString(),
                UserActivity = aboutMeRequest.UserActivity,
            };


            await applicationDbContext.AboutMe.AddAsync(aboutme);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("AboutMe");
        }
            



    }
}
