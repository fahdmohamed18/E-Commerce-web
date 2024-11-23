using Microsoft.AspNetCore.Mvc;
using Natsu.DbContexts;
using Natsu.Helpers;

namespace Natsu.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _context; // Your DbContext

		public CartController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var cart = SessionCart.GetCart(HttpContext);
			return View(cart);
		}

		public IActionResult AddToCart(int productId, int quantity)
		{
			var product = _context.products.Find(productId);
			if (product == null) return NotFound();

			var cart = SessionCart.GetCart(HttpContext);
			cart.AddItem(product, quantity);
			SessionCart.SaveCart(HttpContext, cart);

			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int productId)
		{
			var cart = SessionCart.GetCart(HttpContext);
			cart.RemoveItem(productId);
			SessionCart.SaveCart(HttpContext, cart);

			return RedirectToAction("Index");
		}

		public IActionResult ClearCart()
		{
			var cart = SessionCart.GetCart(HttpContext);
			cart.Clear();
			SessionCart.SaveCart(HttpContext, cart);

			return RedirectToAction("Index");
		}
	}

}
