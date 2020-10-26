using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Repositories.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
           : base(options)
        {
        }

        public DbSet<Shop.Models.ProductModel> Product { get; set; }
    }
}
