using Natsu.Models;
using Microsoft.EntityFrameworkCore;
using Natsu.Models;
namespace Natsu.DbContexts
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> products { get; set; }
		public DbSet<Category> categories { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Review> Reviews { get; set; }


		public ApplicationDbContext(DbContextOptions options) : base(options) 
		{ 

		}
	}
}
