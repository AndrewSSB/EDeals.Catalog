using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class DiscountConfiguration : BaseEntityConfiguration<Discount, int>
    {
        public override void Configure(EntityTypeBuilder<Discount> builder)
        {
            base.Configure(builder);

            builder
                .HasIndex(d => d.DiscountCode)
                .IsUnique();

            builder
                .Property(d => d.DiscountCode)
                .HasMaxLength(10)
                .IsRequired(true);

            builder
                .Property(d => d.DiscountName)
                .HasMaxLength(50);

            builder
                .Property(d => d.Description)
                .HasMaxLength(100);

            builder
                .Property(d => d.DiscountPercent)
                .HasPrecision(4, 2);
        }
    }
}
