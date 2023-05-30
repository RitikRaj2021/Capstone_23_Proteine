using Capstone_23_Proteine.Models; // Import the ErrorViewModel from the Models namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Data;
using System.Diagnostics;
using Capstone_23_Proteine.Data; // Import the namespace that contains ApplicationDbContext
using Capstone_23_Proteine.Models.Domain; // Import the namespace that contains FoodIntake

namespace Capstone_23_Proteine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Add the _context field

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; // Initialize the _context field
        } 

        // GET: /Home/Landing
        public IActionResult Landing()
        {
            return View();
        }

        // GET: /Home/Index
        public IActionResult Index()
        {
            int totalCalories = CalculateTotalCalories(); // Calculate the total calories
            int totalProtein = CalculateTotalProtein(); // Calculate the total protein
            int totalFat = CalculateTotalFat(); // Calculate the total fat

            ViewBag.TotalCalories = totalCalories; // Set the totalCalories value in the ViewBag
            ViewBag.TotalProtein = totalProtein; // Set the totalProtein value in the ViewBag
            ViewBag.TotalFat = totalFat; // Set the totalFat value in the ViewBag

            return View();
        }

        // CalculateTotalProtein Function
        private int CalculateTotalProtein()
        {
            // Logic to calculate the total protein
            DateTime today = DateTime.Today;
            int totalProtein = _context.FoodIntake
                .Where(f => f.Date.Date == today)
                .Sum(f => f.Protein);
            // Return the calculated total protein
            return totalProtein;
        }

        // CalculateTotalFat Function
        private int CalculateTotalFat()
        {
            // Logic to calculate the total fat
            DateTime today = DateTime.Today;
            int totalFat = _context.FoodIntake
                .Where(f => f.Date.Date == today)
                .Sum(f => f.Fat);
            // Return the calculated total fat
            return totalFat;
        }


        // CalculateTotalCalories Function
        private int CalculateTotalCalories()
        {
            // Logic  to calculate the total calories
            DateTime today = DateTime.Today;
            int totalCalories = _context.FoodIntake
                .Where(f => f.Date.Date == today)
                .Sum(f => f.Calories);
            // Return the calculated total calories
            return totalCalories;
        }


        // GET: /Home/Privacy
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }


        // GET: /Home/AboutMe
        public IActionResult AboutMe()
        {
            return View();
        }

        // GET: /Home/MyDetails
        public IActionResult MyDetails()
        {
            return View();
        }

        // GET: /Home/MyRecords
        public IActionResult MyRecords()
        {
            return View();
        }

        public IActionResult Sleep()
        {
            return View();
        }
        public IActionResult Mediterranean_Diet()
        {
            return View();
        }
        public IActionResult Mood()
        {
            return View();
        }

        public IActionResult Crispy_Falafels()
        {
            return View();
        }
        public IActionResult Spanish_pisto()
        {
            return View();
        }
        public IActionResult Veggie_nachos()
        {
            return View();
        }
        public IActionResult Rainbow_chicken()
        {
            return View();
        }
        public IActionResult Lamb()
        {
            return View();
        }
        public IActionResult MEd_salad()
        {
            return View();
        }
        public IActionResult Chicken_and_()
        {
            return View();
        }
        public IActionResult Chicken_pad()
        {
            return View();
        }

        public IActionResult Pie_vegg()
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
