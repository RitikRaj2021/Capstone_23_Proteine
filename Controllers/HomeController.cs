using Capstone_23_Proteine.Models; // Import the ErrorViewModel from the Models namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Data;
using System.Diagnostics;

namespace Capstone_23_Proteine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /Home/Landing
        public IActionResult Landing()
        {
            return View();
        }

        // GET: /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Privacy
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
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

        // GET: /Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create an ErrorViewModel object with the request ID or trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
