using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Interfaces
{
    public interface IShopViewRepository
    {
        Task<List<ProductShopModel>> GetAllAsync();
    }
}
