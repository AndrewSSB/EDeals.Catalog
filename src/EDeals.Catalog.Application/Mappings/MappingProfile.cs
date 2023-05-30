using AutoMapper;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
