using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Entities;
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
            var product = new Product
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price,
                Count = productModel.Count,
                SKU = productModel.SKU,
                CategoryId = productModel.CategoryId
            };

            _context.Add(product);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task DeleteAsync(int? id)
        {
            var productModel = await _context.Products.FindAsync(id);
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _context
                .Products
                .Select(product => new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Count = product.Count,
                    SKU = product.SKU,
                    CategoryId = product.CategoryId,
                    //Category = product.Category
                })
                .ToListAsync();
        }

        public async Task<ProductModel> GetAsync(int? id)
        {
            var productModel = await _context.Products.FindAsync(id);
            var product = new ProductModel
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price,
                Count = productModel.Count,
                SKU = productModel.SKU,
                CategoryId = productModel.CategoryId,
                //CategoryName = productModel.Category.Name
            };
            return product;
        }

        public async Task<bool> ProductExistsAsync(int? id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }

        public async Task<ProductModel> UpdateAsync(ProductModel productModel)
        {
            var product = new Product
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price,
                Count = productModel.Count,
                SKU = productModel.SKU,
                CategoryId = productModel.CategoryId
            };
            _context.Update(product);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<ProductModel>> GeneralFilterAsync(string searchString, int? categoryId, int? minValue, int? maxValue)
        {
            var products = _context.Products.AsQueryable();

            if (minValue <= 0 || maxValue <= 0)
                return null;

            products = await _context.Products
               .Where
               (product =>
               product.CategoryId == categoryId
               || product.Name.Contains(searchString)
               || product.Price >= minValue && product.Price <= maxValue)
               .ToListAsync();

            return await products
                .Select(productModel => new ProductModel
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Price = productModel.Price,
                    Count = productModel.Count,
                    SKU = productModel.SKU,
                    CategoryId = productModel.CategoryId
                })
                .ToListAsync();

            return list;
        }

        public async Task<IList<ProductModel>> SerachProductsAsync(string searchText)
        {
            var products = _context.Products.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                products = products.Where(s => s.Name.Contains(searchText));
            }

            return await products
                .Select(productModel => new ProductModel
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Price = productModel.Price,
                    Count = productModel.Count,
                    SKU = productModel.SKU,
                    CategoryId = productModel.CategoryId
                })
                .ToListAsync();
        }
    }
}