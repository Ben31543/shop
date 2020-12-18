using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ProductPageModel
    {
        public ProductPageModel() { }
        public List<ProductModel> Products { get; set; }
        public ProductCriteria SearchCriteria { get; set; }

        private IProductRepository productRepository;
        public ProductPageModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<ProductModel>> GetProductsAsync(ProductCriteria criteria)
        {
            return await productRepository.GeneralFilterAsync(criteria);
        }
    }
}
