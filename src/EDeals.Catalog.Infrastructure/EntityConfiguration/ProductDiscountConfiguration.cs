using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    internal class ProductDiscountConfiguration : BaseEntityConfiguration<ProductDiscount, int>
    {
        public override void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            base.Configure(builder);
        }
    }
}
