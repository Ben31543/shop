using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Repositories.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public int Count { get; set; }

        public string SKU { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Cart Cart { get; set; }
    }
}
