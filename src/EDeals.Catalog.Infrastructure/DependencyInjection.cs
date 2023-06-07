using EDeals.Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EDeals.Catalog.Infrastructure.Settings;
using EDeals.Catalog.Infrastructure.Shared.DateTimeHelpers;
using EDeals.Catalog.Infrastructure.Shared.ExecutionContext;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Infrastructure.Repositories;
using EDeals.Catalog.Application.Settings;

namespace EDeals.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddDbContext(this IServiceCollection services, DbSettings dbSettings)
        {
            // Check git status

            var connectionString = dbSettings.MySqlConnectionString;
            var db_username = dbSettings.Username;
            var db_password = dbSettings.Password;
            var db_endpoint = dbSettings.Endpoint.Split(":")[0];
            var db_port = dbSettings.Endpoint.Split(":")[1];
            var db_max_pool_size = dbSettings.MaxPoolSize;

            connectionString = string.Format(connectionString, db_endpoint, db_port, db_username, db_password, db_max_pool_size);

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 32)),
                    options => options.EnableRetryOnFailure()
                )
            );

            //services.AddScoped<IAppDbContext, AppDbContext>();
        }

        public static IServiceCollection AddInfrastructureMethods(this IServiceCollection services)
        {
            // Services
            services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddHttpContextAccessor().AddScoped<ICustomExecutionContext, CustomExecutionContext>();

            return services;
        }

        public static IServiceCollection AddInfraConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings)));
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.Configure<AzureSettings>(configuration.GetSection(nameof(AzureSettings)));

            return services;
        }
    }
}
