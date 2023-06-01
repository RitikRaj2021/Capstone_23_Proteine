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
    public class FoodIntakeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public FoodIntakeController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        // GET: /FoodIntake/MyRecords
        [HttpGet]
        public async Task<IActionResult> MyRecords(DateTime? searchDate)
        {
            var userId = userManager.GetUserId(User);
            IQueryable<FoodIntake> foodIntakeQuery = applicationDbContext.FoodIntake.Where(f => f.UserId == userId);

            if (searchDate.HasValue)
            {
                foodIntakeQuery = foodIntakeQuery.Where(f => f.Date.Date == searchDate.Value.Date);
            }

            var foodIntake = await foodIntakeQuery.ToListAsync();
            return View(foodIntake);
        }

        // GET: /FoodIntake/FoodIntake
        [HttpGet]
        public IActionResult FoodIntake()
        {
            // Render the FoodIntake view
            return View();
        }

        // POST: /FoodIntake/FoodIntake
        [HttpPost]
        public async Task<IActionResult> FoodIntake(FoodIntakeViewModel foodIntakeRequest)
        {
            // Retrieve the current user
            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

            // Create a new FoodIntake object with the user-provided data
            var foodIntake = new FoodIntake()
            {
                ID = Guid.NewGuid(),
                UserId = userId,
                Protein = foodIntakeRequest.Protein,
                Calories = foodIntakeRequest.Calories,
                Fat = foodIntakeRequest.Fat,
                MealType = foodIntakeRequest.MealType,
                MealName = foodIntakeRequest.MealName,
                Date = foodIntakeRequest.Date
            };

            // Add the FoodIntake object to the FoodIntake DbSet and save changes to the database
            await applicationDbContext.FoodIntake.AddAsync(foodIntake);
            await applicationDbContext.SaveChangesAsync();

            // Redirect to the MyRecords action to display the updated food intake records
            return RedirectToAction("MyRecords");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Retrieve the FoodIntake record to be deleted
            var foodIntake = await applicationDbContext.FoodIntake.FindAsync(id);

            // Check if the record exists
            if (foodIntake == null)
            {
                return NotFound();
            }

            // Remove the record from the DbSet and save changes
            applicationDbContext.FoodIntake.Remove(foodIntake);
            await applicationDbContext.SaveChangesAsync();

            // Redirect to the MyRecords action to display the updated food intake records
            return RedirectToAction("MyRecords");
        }

       /* [HttpPost]
        public async Task<IActionResult> SetGoals(SetGoalsViewModel SetGoalsRequest)
        {
            // Retrieve the current user
            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

            // Create a new FoodIntake object with the user-provided data
            var setGoals = new SetGoals()
            {
                ID = Guid.NewGuid(),
                UserId = userId,
                SetProtein = SetGoalsRequest.SetProtein,
                SetCalories = SetGoalsRequest.SetCalories,
                SetFat = SetGoalsRequest.SetFat,
            };

            // Add the FoodIntake object to the FoodIntake DbSet and save changes to the database
            await applicationDbContext.SetGoals.AddAsync(setGoals);
            await applicationDbContext.SaveChangesAsync();

            // Redirect to the MyRecords action to display the updated food intake records
            return RedirectToAction("Index");
        }*/

    }
}
