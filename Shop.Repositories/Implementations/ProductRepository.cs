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
                .Include(x => x.Category)
                .Select(product => new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Count = product.Count,
                    SKU = product.SKU,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name
                })
                .ToListAsync();
        }

        public async Task<ProductModel> GetAsync(int? id)
        {
            return await _context.Products
                .Include(x => x.Category)
                .Select(productModel => new ProductModel
                {
	                Id = productModel.Id,
	                Name = productModel.Name,
	                Price = productModel.Price,
	                Count = productModel.Count,
	                SKU = productModel.SKU,
	                CategoryId = productModel.CategoryId,
	                CategoryName = productModel.Category.Name
                })
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<List<ProductModel>> GeneralFilterAsync(ProductCriteria criteria)
        {
            var products = _context.Products.AsQueryable().AsNoTracking();

            if (criteria != null)
            {
	            if (!String.IsNullOrWhiteSpace(criteria.SearchString))
	            {
		            products = products.Where(prod => prod.Name.Contains(criteria.SearchString));
	            }

	            if (criteria.CategoryId.HasValue)
	            {
		            products = products.Where(prod => prod.CategoryId == criteria.CategoryId);
	            }

	            if (criteria.MinValue >= 0 && criteria.MaxValue > 0)
	            {
		            products = products.Where(prod => prod.Price >= criteria.MinValue && prod.Price <= criteria.MaxValue);
	            }
            }

            return await products
                .Select(productModel => new ProductModel
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Price = productModel.Price,
                    Count = productModel.Count,
                    SKU = productModel.SKU,
                    CategoryId = productModel.CategoryId,
                    CategoryName = productModel.Category.Name
                })
                .ToListAsync();
        }
    }
}