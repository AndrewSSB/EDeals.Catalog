using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class ShoppingSessionConfiguration : BaseEntityConfiguration<ShoppingSession, int>
    {
        public override void Configure(EntityTypeBuilder<ShoppingSession> builder)
        {
            base.Configure(builder);

            builder
                .Property(ss => ss.Total)
                .HasPrecision(10, 2);

            builder
                .HasMany(ss => ss.CartItems)
                .WithOne(ci => ci.ShoppingSession)
                .HasForeignKey(ci => ci.ShoppingSessionId);
        }
    }
}
