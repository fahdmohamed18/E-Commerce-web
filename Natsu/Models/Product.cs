using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Natsu.Models
{
    public class Product
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Product name is required.")]
		public string Name { get; set; }

		[StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
		public string Description { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
		public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "choose a valid category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string? ImagePath { get; set; }
        [NotMapped] // mean dont mapped it to db
        [Display(Name = "Image")] // it is change it in everyware
        [ValidateNever]
        public IFormFile ImageFile { get; set; }

		// Optional: Additional navigation properties for purchases
		
	}
}
