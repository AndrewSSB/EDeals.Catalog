using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Interfaces.Email;
using EDeals.Catalog.Application.Mappings;
using EDeals.Catalog.Application.Services;
using EDeals.Catalog.Application.Services.EmailServices;
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
            services.AddTransient<IProductReviewService, ProductReviewService>();
            services.AddTransient<IUserInfoService, UserInfoService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IBaseEmailService, BaseEmailService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddMappings();

            return services;
        }

        public static IServiceCollection AddApplicationConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureSettings>(configuration.GetSection(nameof(AzureSettings)));
            services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));

            return services;
        }
    }
}
