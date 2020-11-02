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
    class CategoryRepository : ICategoryRepository
    {
        private readonly ShopContext _context;

        public CategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public  bool CategoryExists(int? id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel categoryModel)
        {
            _context.Add(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task DeleteAsync(int? id)
        {
            var categoryModel = await _context.Category.FindAsync(id);
            _context.Category.Remove(categoryModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<CategoryModel> GetAsync(int? id)
        {
            return await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel categoryModel)
        {
            _context.Update(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }
    }
}
