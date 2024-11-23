using Microsoft.AspNetCore.Mvc;
using Natsu.DbContexts;
using Natsu.Models;

namespace Natsu.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

       public IActionResult Index()
{
            var reviews = _context.Reviews.ToList() ?? new List<Review>(); // Ensure it's not null
        return View(reviews);
}


        [HttpPost]
        public IActionResult SubmitReview(Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                // Return the same view with updated reviews
                var updatedReviews = _context.Reviews.ToList();
                return RedirectToAction("About", "Home" , updatedReviews);

            }
            return View("About", "Home");
        }

    }
}
