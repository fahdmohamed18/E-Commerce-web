using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Natsu.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public int Quantity { get; set; }

		public int ProductId { get; set; }
		[ValidateNever]
		public virtual Product Product { get; set; } // Assuming Product is your product model
	}
	public class ShoppingCart
	{
		
			public List<CartItem> Items { get; set; } = new List<CartItem>();

			public void AddItem(Product product, int quantity)
			{
				var cartItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
				if (cartItem == null)
				{
					Items.Add(new CartItem { ProductId = product.Id, Quantity = quantity, Product = product });
				}
				else
				{
					cartItem.Quantity += quantity;
				}
			}

			public void RemoveItem(int productId)
			{
				var cartItem = Items.FirstOrDefault(i => i.ProductId == productId);
				if (cartItem != null)
				{
					Items.Remove(cartItem);
				}
			}

			public void Clear()
			{
				Items.Clear();
			}

			public decimal Total()
			{
				return Items.Sum(i => i.Product.Price * i.Quantity); // Assuming Product has a Price property
			}
		}


	
}
