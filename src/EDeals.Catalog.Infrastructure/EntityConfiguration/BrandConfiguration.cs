using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class BrandConfiguration : BaseEntityConfiguration<Brand, int>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);

            builder
                .Property(b => b.BrandName)
                .HasMaxLength(50);

            builder
                .HasMany(b => b.Products)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.BrandId);
        }
    }
}
