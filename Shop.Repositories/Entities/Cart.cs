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

        public int Count { get; set; }

        public DateTime DateAdded { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}