using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Natsu.DbContexts; // Ensure you have this namespace
using Natsu.Models;
using System.Diagnostics;

namespace Natsu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Add this line

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context) // Add context parameter
        {
            _logger = logger;
            _context = context; // Initialize context
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var reviews = _context.Reviews.ToList() ?? new List<Review>(); // Ensure it's not null
            return View(reviews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
