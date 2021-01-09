using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCartAsync(ProductModel model, int productCount);

        Task<List<CartItemModel>> CartViewAsync();

        Task<CartItemModel> GetCartItemAsync(int? id);

        Task<CartItemModel> UpdateAsync(CartItemModel cartModel);

        Task DeleteAsync(int? id);
    }
}
