using Capstone_23_Proteine.Models; // Import the ErrorViewModel from the Models namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Data;
using System.Diagnostics;
using Capstone_23_Proteine.Data; // Import the namespace that contains ApplicationDbContext
using Capstone_23_Proteine.Models.Domain; // Import the namespace that contains FoodIntake
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Capstone_23_Proteine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; // Add this line

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager) // Add `UserManager<IdentityUser> userManager` parameter
        {
            _logger = logger;
            _context = context;
            _userManager = userManager; // Initialize the _userManager field
        }

        // GET: /Home/Landing
        public IActionResult Landing()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        // GET: /Home/Index
        // GET: /Home/Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the current user
                var user = _userManager.GetUserAsync(User).Result;
                var userId = user.Id;

                // Retrieve the SetGoals record for the current user
                var setGoals = await _context.SetGoals.FirstOrDefaultAsync(g => g.UserId == userId);

                // Set the SetProtein, SetCalories, and SetFat values in the ViewBag
                ViewBag.SetProtein = setGoals?.SetProtein;
                ViewBag.SetCalories = setGoals?.SetCalories;
                ViewBag.SetFat = setGoals?.SetFat;

                // Retrieve the SetGoals record for the current user
                var aboutMe = await _context.AboutMe.FirstOrDefaultAsync(g => g.UserId == userId);
                // Set the SetProtein, SetCalories, and SetFat values in the ViewBag
                ViewBag.FirstName = aboutMe?.FirstName;
                ViewBag.LastName = aboutMe?.LastName;
                ViewBag.Height = aboutMe?.Height;
                ViewBag.Weight = aboutMe?.Weight;
                ViewBag.DietaryOptions = aboutMe?.DietaryOptions;

                int totalCalories = CalculateTotalCalories(); // Calculate the total calories
                int totalProtein = CalculateTotalProtein(); // Calculate the total protein
                int totalFat = CalculateTotalFat(); // Calculate the total fat                    

                ViewBag.TotalCalories = totalCalories; // Set the totalCalories value in the ViewBag
                ViewBag.TotalProtein = totalProtein; // Set the totalProtein value in the ViewBag
                ViewBag.TotalFat = totalFat; // Set the totalFat value in the ViewBag

                if (setGoals?.SetFat != null && totalFat >= int.Parse(setGoals.SetFat))
                {
                    ViewBag.FatMessage = "Warning: Total fat intake exceeds or equals the set fat goal!";
                }


                if (setGoals?.SetCalories != null && totalCalories >= int.Parse(setGoals.SetCalories))
                {
                    ViewBag.CaloriesMessage = "Warning: Total Calories intake exceeds or equals the set fat goal!";
                }

                if (setGoals?.SetProtein != null && totalProtein >= int.Parse(setGoals.SetProtein))
                {
                    ViewBag.ProteinMessage = "Warning: Total Protein intake exceeds or equals the set fat goal!";
                }

                int notificationCount = 0; // Initialize the count
                if (!string.IsNullOrEmpty(ViewBag.FatMessage))
                {
                    notificationCount++;
                }
                if (!string.IsNullOrEmpty(ViewBag.CaloriesMessage))
                {
                    notificationCount++;
                }
                if (!string.IsNullOrEmpty(ViewBag.ProteinMessage))
                {
                    notificationCount++;
                }
                ViewBag.NotificationCount = notificationCount; // Set the count in the ViewBag


                return View();
            }

            // Add a return statement here
            return RedirectToAction("Landing");
        }



        // CalculateTotalProtein Function
        private int CalculateTotalProtein()
        {
            // Logic to calculate the total protein
            DateTime today = DateTime.Today;
            var user = _userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            int totalProtein = _context.FoodIntake
                .Where(f => f.Date.Date == today && f.UserId == userId)
                .Sum(f => f.Protein);
            // Return the calculated total protein
            return totalProtein;
        }

        // CalculateTotalFat Function
        private int CalculateTotalFat()
        {
            // Logic to calculate the total fat
            DateTime today = DateTime.Today;
            var user = _userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            int totalFat = _context.FoodIntake
                .Where(f => f.Date.Date == today && f.UserId == userId)
                .Sum(f => f.Fat);
            // Return the calculated total fat
            return totalFat;
        }

        // CalculateTotalCalories Function
        private int CalculateTotalCalories()
        {
            // Logic  to calculate the total calories
            DateTime today = DateTime.Today;
            var user = _userManager.GetUserAsync(User).Result;
            var userId = user.Id;
            int totalCalories = _context.FoodIntake
                .Where(f => f.Date.Date == today && f.UserId == userId)
                .Sum(f => f.Calories);
            // Return the calculated total calories
            return totalCalories;
        }


        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        // GET: /Home/AboutMe
        [Authorize]
        public IActionResult AboutMe()
        {
            return View();
        }
        [Authorize]
        // GET: /Home/MyDetails
        public IActionResult MyDetails()
        {
            return View();
        }

        // GET: /Home/MyRecords
        [Authorize]
        public IActionResult MyRecords()
        {
            return View();
        }

        public IActionResult ContactUsConfirm()
        {
            return View();
        }

        [Authorize]
        public IActionResult Sleep()
        {
            return View();
        }
        [Authorize]
        public IActionResult Mediterranean_Diet()
        {
            return View();
        }
        [Authorize]
        public IActionResult Mood()
        {
            return View();
        }

        [Authorize]
        public IActionResult Crispy_Falafels()
        {
            return View();
        }
        [Authorize]
        public IActionResult Spanish_pisto()
        {
            return View();
        }
        [Authorize]
        public IActionResult Veggie_nachos()
        {
            return View();
        }
        [Authorize]
        public IActionResult Rainbow_chicken()
        {
            return View();
        }
        [Authorize]
        public IActionResult Lamb()
        {
            return View();
        }
        [Authorize]
        public IActionResult MEd_salad()
        {
            return View();
        }
        [Authorize]
        public IActionResult Chicken_and_()
        {
            return View();
        }
        [Authorize]
        public IActionResult Chicken_pad()
        {
            return View();
        }
            
        public IActionResult Pie_vegg()
        {
            return View();
        }   

        public IActionResult SetGoals()
        {
            return View();
        }

        // GET: /Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create an ErrorViewModel object with the request ID or trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
