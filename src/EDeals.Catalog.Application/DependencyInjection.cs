using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Mappings;
using EDeals.Catalog.Application.Services;
using EDeals.Catalog.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EDeals.Catalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationMethods(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUploadPhotosService, UploadPhotosService>();
            services.AddTransient<ICartItemService, CartItemService>();
            services.AddTransient<IShoppingSessionService, ShoppingSessionService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IFavouriteService, FavouriteService>();
            services.AddTransient<IStripeService, StripeService>();
            services.AddTransient<IMessageService, MessageService>();

            services.AddMappings();

            return services;
        }

        public static IServiceCollection AddApplicationConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureSettings>(configuration.GetSection(nameof(AzureSettings)));

            return services;
        }
    }
}
