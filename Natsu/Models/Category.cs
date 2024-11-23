using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Natsu.Models;

namespace Natsu.Models
{
    public class Category
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Category name is required.")]
		public string Name { get; set; }

		[StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
		public string Description { get; set; }

        //navigation proprty
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}


