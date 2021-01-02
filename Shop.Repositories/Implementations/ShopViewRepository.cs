using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Repositories.Implementations
{
    public class ShopViewRepository : IShopViewRepository
    {
        private readonly ShopContext _context;

        public ShopViewRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductShopModel>> GetAllAsync()
        {
            return await _context
                .Products
                .Include(x => x.Category)
                .Select(product => new ProductShopModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category.Name
                })
                .ToListAsync();
        }
    }
}
