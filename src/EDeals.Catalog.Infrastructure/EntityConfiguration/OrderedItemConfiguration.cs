using EDeals.Catalog.Domain.Entities.TransactionDetails;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class OrderedItemConfiguration : BaseEntityConfiguration<OrderedItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderedItem> builder)
        {
            base.Configure(builder);


        }
    }
}
