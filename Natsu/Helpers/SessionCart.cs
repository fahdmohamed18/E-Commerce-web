using Natsu.Models;
using System.Text.Json;

namespace Natsu.Helpers
{
	public static class SessionCart
	{
		private const string CartSessionKey = "ShoppingCart";

		public static ShoppingCart GetCart(HttpContext httpContext)
		{
			var cart = httpContext.Session.GetString(CartSessionKey);
			if (string.IsNullOrEmpty(cart))
			{
				return new ShoppingCart();
			}

			return JsonSerializer.Deserialize<ShoppingCart>(cart);
		}

		public static void SaveCart(HttpContext httpContext, ShoppingCart cart)
		{
			var json = JsonSerializer.Serialize(cart);
			httpContext.Session.SetString(CartSessionKey, json);
		}
	}

}
