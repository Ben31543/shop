using Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Repositories.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public List<Product> Products { get; set; }

        public decimal TotalSum { get; set; }

        public DateTime DateAdded { get; set; }
    }
}