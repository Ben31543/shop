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
                .Include(x => x.Products)
                .Select(s => new CategoryModel
                {
                    Name = s.Name,
                    Id = s.Id
                })
                .ToListAsync();
        }

        public async Task<CategoryModel> GetAsync(int? id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);

            var category = new CategoryModel
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            };

            return category;
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel categoryModel)
        {
            var category = new Category
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            };
            _context.Update(category);
            await _context.SaveChangesAsync();
            return categoryModel;
        }
    }
}
