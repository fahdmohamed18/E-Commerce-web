using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Natsu.DbContexts;
using Natsu.Models;

namespace Natsu.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetIndexView()
        {
            return View("Index", _context.categories.ToList());
        }

        [HttpGet]

        public IActionResult GetCreateView()
        {
            ViewBag.AllCategories = _context.categories.ToList();
            return View("Create");
        }

        [HttpGet]

        public IActionResult GetDetailsView(int id)
        {

            var category = _context.categories.Include(prod => prod.Products).FirstOrDefault(prod1 => prod1.Id == id);
            if (category == null)
            {
                return
                    NotFound();
            }
            else
            {
                return View("Details", category);
            }
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");
        }

        // Edit category (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View("Edit", category);
        }

        // Edit category (POST)
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }

            return View("Edit", category);
        }

        // Delete category (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View("Delete", category);
        }

        // Delete category (POST)
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");
        }
    }
}

