using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Mappings;
using EDeals.Catalog.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EDeals.Catalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationMethods(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddMappings();

            return services;
        }
    }
}
