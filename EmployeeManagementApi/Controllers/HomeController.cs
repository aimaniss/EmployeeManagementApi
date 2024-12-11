using System.Diagnostics;
using EmployeeManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Home Page
        public IActionResult Index()
        {
            return View();  // Returns the view for the home page
        }

        // Error Page (In case of an error in the application)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Logs any error and returns an ErrorViewModel with the request id.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // About Us Page (added new action)
        public IActionResult AboutUs()
        {
            return View();  // Returns the view for the About Us page
        }

        // FAQ Page (added new action)
        public IActionResult FAQ()
        {
            return View();  // Returns the view for the FAQ page
        }
    }
}
