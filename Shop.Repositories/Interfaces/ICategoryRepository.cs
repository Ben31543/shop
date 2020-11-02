using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel> GetAsync(int? id);
        Task DeleteAsync(int? id);
        Task<CategoryModel> UpdateAsync(CategoryModel categoryModel);
        Task<CategoryModel> CreateAsync(CategoryModel categoryModel);
        bool CategoryExists(int? id);
    }
}
