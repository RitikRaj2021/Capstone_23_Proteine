using Microsoft.AspNetCore.Mvc;
using Capstone_23_Proteine.Models;
using Capstone_23_Proteine.Data;
using Capstone_23_Proteine.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
        public async Task<IActionResult> MyRecords()
        {

            var userId = userManager.GetUserId(User);
            var foodIntake = await applicationDbContext.FoodIntake.Where(f => f.UserId == userId).ToListAsync();
            return View(foodIntake);

            /*
            // Retrieve all FoodIntake records from the database
            var foodIntake = await applicationDbContext.FoodIntake.ToListAsync();

            // Pass the foodIntake records to the view for display
            return View(foodIntake);*/
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
                MealName = foodIntakeRequest.MealName,
                Date = foodIntakeRequest.Date
            };

            // Add the FoodIntake object to the FoodIntake DbSet and save changes to the database
            await applicationDbContext.FoodIntake.AddAsync(foodIntake);
            await applicationDbContext.SaveChangesAsync();

            // Redirect to the MyRecords action to display the updated food intake records
            return RedirectToAction("MyRecords");
        }
    }
}
