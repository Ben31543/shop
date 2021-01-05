using Microsoft.Extensions.DependencyInjection;
using Shop.Repositories.Implementations;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories.Injections
{
    public static class RepositoryInjections
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
        }
    }
}
