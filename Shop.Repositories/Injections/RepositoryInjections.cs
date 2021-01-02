using Microsoft.Extensions.DependencyInjection;
using Shop.Repositories.Implementations;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Repositories.Injections
{
    public static class RepositoryInjections
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IShopViewRepository, ShopViewRepository>();
        }
    }
}
