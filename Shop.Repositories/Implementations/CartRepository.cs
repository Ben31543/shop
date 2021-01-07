using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Repositories.Entities;

namespace Shop.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ShopContext _context;

        public CartRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(ProductModel model, int productCount)
        {
            var cartItem = new Cart
            {
                ProductId = model.Id,
                Count = productCount,
                DateAdded = DateTime.Now
            };

            _context.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItemModel>> CartViewAsync()
        {
            return await _context.Cart
                .Include(x => x.Product)
                .Select(x => new CartItemModel
                {
                    Count = x.Count,
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price = x.Product.Price
                }).ToListAsync();
        }
    }
}
