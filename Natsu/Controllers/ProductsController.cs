using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Natsu.DbContexts;
using Natsu.Models;

namespace Natsu.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        // Http verbs : httpGet httpPost
        [HttpGet]

        public IActionResult GetCreateView()
        {
            ViewBag.AllCategories = _context.categories.ToList();
            return View("Create");
        }
        [HttpGet]

        //collections
        public IActionResult View()
        {
            return View("view", _context.products.ToList());

        }
        [HttpGet]

        //product
        public IActionResult GetIndexView()
        {
            return View("Index", _context.products.ToList());
        }
        
        
        [HttpGet]

        public IActionResult GetDetailsView(int id)
        {
            Product product = _context.products.Include(prod => prod.Category).FirstOrDefault(prod1 => prod1.Id == id);
            if (product == null)
            {
                return
                    NotFound();
            }
            else
            {
                return View("Details", product);
            }
        }
        [HttpPost]
        public IActionResult AddNew(Product product)
        {
       
            if (ModelState.IsValid == true)
            {
                if (product.ImageFile == null)
                {
                    product.ImagePath = "\\images\\No_image.png";
                }
                else
                {
                    
                    Guid imgGuid = Guid.NewGuid();
                    string imgExtinsion = Path.GetExtension(product.ImageFile.FileName);
                    string imagePath = "\\images\\" + imgGuid + imgExtinsion;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imagePath;
                    FileStream fileStream = new FileStream(imgFullPath, FileMode.Create);
                    product.ImageFile.CopyTo(fileStream);
                    fileStream.Dispose();
                    product.ImagePath = imagePath;

                }

             

                _context.products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllCategories = _context.categories.ToList();
                return View("Create");
            }
        }

        [HttpGet]

        public IActionResult GetDeleteView(int id)
        {
            Product product = _context.products.Include(prod => prod.Category).FirstOrDefault(prod => prod.Id == id);
            if (product == null)
            {
                return
                    NotFound();
            }
            else
            {
                return View("Delete", product);
            }



        }

        [HttpPost]
        public IActionResult DeleteCurrent(int Id)
        {
            // Find()-> only search for prymary keys but it's really fast as it get data direcrly from memory

            Product product = _context.products.Find(Id);
            // fully qualified name
            if (product.ImagePath != "\\images\\No_Image.png")
            {
                if (System.IO.File.Exists(product.ImagePath))

                {
                    System.IO.File.Delete(product.ImagePath);
                }
            }
            _context.products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");

        }
        [HttpGet]

        public IActionResult GetEditView(int id)
        {
            Product product = _context.products.Find(id);
            if (product == null)
            {
                return
                    NotFound();
            }
            else
            {
                ViewBag.AllCategories = _context.categories.ToList();
                return View("Edit", product);
            }

        }

       

        [HttpPost]
        public IActionResult EditCurrent(Product product)
        {
            if (ModelState.IsValid == true)
            {
                if (product.ImageFile != null)
                {
                    if (product.ImagePath != "\\images\\No_Image.png")
                    {
                        if (System.IO.File.Exists(_webHostEnvironment.WebRootPath + product.ImagePath) == true)
                        {
                            System.IO.File.Delete(_webHostEnvironment.WebRootPath + product.ImagePath);
                        }
                    }

                    Guid imgGuid = Guid.NewGuid();
                    string imgExtension = Path.GetExtension(product.ImageFile.FileName);
                    string imgPath = "\\images\\" + imgGuid + imgExtension;
                    product.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
                    FileStream fileStream = new FileStream(imgFullPath, FileMode.Create);
                    product.ImageFile.CopyTo(fileStream);
                    fileStream.Dispose();
                }

              
                _context.products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllCategories = _context.categories.ToList();
                return View("Edit");
            }
        }
    }
}

