using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(ProductModel model, int productCount);
        Task<List<CartItemModel>> CartViewAsync();
    }
}
