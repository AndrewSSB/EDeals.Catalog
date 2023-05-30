using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EDeals.Catalog.Application.Mappings
{
    public static class MappingExtension
    {
        public static void AddMappings(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
