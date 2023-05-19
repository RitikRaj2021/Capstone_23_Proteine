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

        [HttpGet]
        public async Task<IActionResult> MyRecords()
        {
            var foodIntake = await applicationDbContext.FoodIntake.ToListAsync();
            return View(foodIntake);
        }

        [HttpGet]
        public IActionResult FoodIntake()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FoodIntake(FoodIntakeViewModel foodIntakeRequest)
        {
            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

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

            await applicationDbContext.FoodIntake.AddAsync(foodIntake);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("MyRecords");
        }
    }
}