using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ShopContext _context;

        public CartRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task AddToCart(ProductModel model, int productCount)
        {
            var product = new CartItemModel
            {
                Name = model.Name,
                Count = productCount,
                Price = model.Price
            };

            _context.Add(product);
        }

        public async Task<List<CartItemModel>> CartView()
        {
            return await _context.Cart
                .Include(x => x.Products)
                .Select(product => new CartItemModel
                {
                    Name = product.ProductName,
                    Count = product.Products.Count,
                    Price = product.TotalSum
                })
                .ToListAsync();
        }
    }
}
