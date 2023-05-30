using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class SellerConfiguration : BaseEntityConfiguration<Seller, int>
    {
        public override void Configure(EntityTypeBuilder<Seller> builder)
        {
            base.Configure(builder);

            builder
                .Property(s => s.SellerName)
                .HasMaxLength(50);
        }
    }
}
