using Microsoft.AspNetCore.Mvc;
using Capstone_23_Proteine.Models;
using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;


namespace Capstone_23_Proteine.Controllers
{

    public class SetGoalsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public SetGoalsController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        // GET: /SetGoals/SetGoals
        [HttpGet]
        public IActionResult SetGoals()
        {
            // Render the FoodIntake view
            return View();
        }

        // POST: /SetGoals/SetGoals
        [HttpPost]
        public async Task<IActionResult> SetGoals([Bind("SetProtein, SetCalories, SetFat")] SetGoalsViewModel setGoalsRequest)

        {
            // Retrieve the current user
            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

            // Create a new SetGoals object with the user-provided data
            var setGoals = new SetGoals()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SetProtein = setGoalsRequest.SetProtein,
                SetCalories = setGoalsRequest.SetCalories,
                SetFat = setGoalsRequest.SetFat,
               
            };

            // Add the SetGoals object to the SetGoals DbSet and save changes to the database
            await applicationDbContext.SetGoals.AddAsync(setGoals);
            await applicationDbContext.SaveChangesAsync();

            // Redirect to the Index action to display the updated food intake records
            return RedirectToAction("Index");
        }
    }
}
