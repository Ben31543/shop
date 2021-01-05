using System.Collections.Generic;

namespace Shop.Models
{
    public class ProductPageModel
    {
        public List<ProductModel> Products { get; set; }

        public ProductCriteriaModel SearchCriteria { get; set; }
    }
}
