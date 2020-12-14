using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ProductPageModel
    {
        private IProductRepository productRepository;
        public ProductPageModel()
        {

        }
        public ProductPageModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public List<ProductModel> Products { get; set; }

        public ProductCriteria SearchCriteria { get; set; }

        public async Task<List<ProductModel>> GetProductsAsync(ProductCriteria criteria)
        {
            return await productRepository.GeneralFilterAsync(SearchCriteria.SearchString,
                SearchCriteria.CategoryId,
                SearchCriteria.MinValue,
                SearchCriteria.MaxValue);
        }
    }
}
