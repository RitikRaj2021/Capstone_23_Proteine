﻿using Capstone_23_Proteine.Models; // Import the ErrorViewModel from the Models namespace
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

        public IActionResult ContactUs()
        {
            return View();
        }

        // GET: /Home/Index
        [Authorize]
        public IActionResult Index()
        {
            int totalCalories = CalculateTotalCalories(); // Calculate the total calories
            int totalProtein = CalculateTotalProtein(); // Calculate the total protein
            int totalFat = CalculateTotalFat(); // Calculate the total fat          

            // Retrieve the user-specific data, for example, from a database
            AboutMeViewModel aboutMeData = GetAboutMeDataForLoggedInUser();

            //string etCalories = setCalories;

            ViewBag.TotalCalories = totalCalories; // Set the totalCalories value in the ViewBag
            ViewBag.TotalProtein = totalProtein; // Set the totalProtein value in the ViewBag
            ViewBag.TotalFat = totalFat; // Set the totalFat value in the ViewBag

            /*ViewBag.SetCalories = setCalories; // Set the totalCalories value in the ViewBag
            ViewBag.SetProtein = setProtein; // Set the totalProtein value in the ViewBag
            ViewBag.SetFat = setFat; // Set the totalFat value in the ViewBag*/

            // Pass the data to the view using ViewBag
            ViewBag.FirstName = aboutMeData.FirstName;
            ViewBag.Gender = aboutMeData.Gender;
            ViewBag.Height = aboutMeData.Height;
            ViewBag.Weight = aboutMeData.Weight;
            ViewBag.DateOfBirth = aboutMeData.DateOfBirth;
            ViewBag.DietaryOptions = aboutMeData.DietaryOptions;
            ViewBag.UserActivity = aboutMeData.UserActivity;

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

        private AboutMeViewModel GetAboutMeDataForLoggedInUser()
        {
            // Identify the logged-in user (example using User.Identity.Name)
            string loggedInUserName = User.Identity.Name;

            // Retrieve user data from the database using the logged-in user's username or ID
            User user = _context.Users.FirstOrDefault(u => u.UserName == loggedInUserName);

            // Check if the user exists in the database
            if (user != null)
            {
                // Map the user data to the AboutMeViewModel object
                AboutMeViewModel aboutMeData = new AboutMeViewModel
                {
                    FirstName = user.FirstName,
                    Gender = user.Gender,
                    Height = user.Height,
                    Weight = user.Weight,
                    DateOfBirth = user.DateOfBirth,
                    DietaryOptions = user.DietaryOptions,
                    UserActivity = user.UserActivity
                };

                return aboutMeData;
            }

            // If the user does not exist, return null or handle the situation accordingly
            return null;
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

        public IActionResult goalSet()
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
