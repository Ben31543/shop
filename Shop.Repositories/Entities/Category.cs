using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop.Models;

namespace Shop.Repositories.Entities
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Product> Products { get; set; }
	}
}
