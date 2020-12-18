using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetAsync(int? id);
        Task DeleteAsync(int? id);
        Task<ProductModel> UpdateAsync(ProductModel productModel);
        Task<ProductModel> CreateAsync(ProductModel productModel);
        Task<List<ProductModel>> GeneralFilterAsync(ProductCriteria criteria);
    }
}
