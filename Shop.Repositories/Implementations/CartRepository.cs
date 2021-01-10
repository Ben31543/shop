using System;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
                Count = productCount
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
                    Id = x.Id,
                    Count = x.Count,
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price = x.Product.Price
                })
                .ToListAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var cartItemModel = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cartItemModel);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItemModel> GetCartItemAsync(int? id)
        {
            var cartItem = await _context.Cart
                .Include(x => x.Product)
                .Select(x => new CartItemModel
                {
                    Id = x.Id,
                    Name = x.Product.Name,
                    Count = x.Count,
                    Price = x.Product.Price,
                    ProductId = x.ProductId
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            return cartItem;
        }

        public async Task<CartItemModel> UpdateAsync(CartItemModel cartModel)
        {
            var cart = new Cart
            {
                Id = cartModel.Id,
            };

            _context.Attach(cart);
            cart.Count = cartModel.Count;
            await _context.SaveChangesAsync();
            return cartModel;
        }

        public async Task<CartItemModel> UpdateAsync1(CartItemModel cartModel)
        {
	        var cart = await _context.Cart.FirstOrDefaultAsync(s => s.Id == cartModel.Id);

	        if (cart == null)
	        {
		        throw new ArgumentException();
	        }

	        cart.Count = cartModel.Count;
	        await _context.SaveChangesAsync();
	        return cartModel;
        }
    }
}
