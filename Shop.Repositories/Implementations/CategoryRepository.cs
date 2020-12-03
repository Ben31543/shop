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
    class CategoryRepository : ICategoryRepository
    {
        private readonly ShopContext _context;

        public CategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExistsAsync(int? id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel categoryModel)
        {
            var category = new Category
            {
                Name = categoryModel.Name,
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            categoryModel.Id = category.Id;
            return categoryModel;
        }

        public async Task DeleteAsync(int? id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryModel>> GetAllAsync()
        {
            return await _context
	            .Categories
	            .Select(s=>new CategoryModel
	            {
                    Name = s.Name,
                    Id = s.Id
	            })
	            .ToListAsync();
        }

        public List<CategoryModel> GetAll()
        {
            return _context.Categories.ToList();
        }

        public async Task<CategoryModel> GetAsync(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel categoryModel)
        {
            _context.Update(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }
    }
}
