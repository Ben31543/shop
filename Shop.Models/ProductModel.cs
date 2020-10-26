using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required, StringLength(8)]
        public string SKU { get; set; }

    }
}
