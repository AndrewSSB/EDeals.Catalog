using EDeals.Catalog.Domain.Entities.TransactionDetails;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class OrderConfiguration : BaseEntityConfiguration<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder
                .Property(o => o.Total)
                .HasPrecision(10, 2);

            builder
                .HasMany(o => o.OrderedItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
