using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class CartItemConfiguration : BaseEntityConfiguration<CartItem, int>
    {
        public override void Configure(EntityTypeBuilder<CartItem> builder)
        {
            base.Configure(builder);
        }
    }
}
