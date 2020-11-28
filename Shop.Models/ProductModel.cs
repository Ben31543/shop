using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required, StringLength(6)]
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}