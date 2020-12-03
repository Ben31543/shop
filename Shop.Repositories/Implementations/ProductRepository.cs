using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> CreateAsync(ProductModel productModel)
        {
            _context.Add(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task DeleteAsync(int? id)
        {
            var productModel = await _context.Product.FindAsync(id);
            _context.Product.Remove(productModel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ProductModel> GetAsync(int? id)
        {
            return await _context.Product.Include(x => x.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> ProductExistsAsync(int? id)
        {
            return await _context.Product.AnyAsync(e => e.Id == id);
        }

        public async Task<ProductModel> UpdateAsync(ProductModel productModel)
        {
            _context.Update(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<ProductModel>> GetAllWithCategoriesAsync()
        {
            return await _context.Product.Include(x => x.Category).ToListAsync();
        }

        public async Task<List<ProductModel>> GeneralFilterAsync(string searchString, int? categoryId, int? minValue, int? maxValue)
        {
            var products = await GetAllWithCategoriesAsync();

            if (minValue <= 0 || maxValue <= 0)
                return products;

            var byPrice = _context.Product.Where(product => product.Price >= minValue && product.Price <= maxValue).ToList();
            var byName = _context.Product.Where(product => product.Name.Contains(searchString)).ToList();
            var byCategory = _context.Product.Where(product => product.CategoryId == categoryId).ToList();

            products = byCategory.Concat(byName).Concat(byPrice).ToList();

            return products;
        }

        public async Task<IList<ProductModel>> SerachProductsAsync(string searchText)
        {
            var products = _context.Product.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                products = products.Where(s => s.Name.Contains(searchText));
            }

            return await products
                .Select(s=>new ProductModel
                {
                    Id = s.Id,
                    //...
                })
                .ToListAsync();
        }
    }
}