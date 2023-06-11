using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class FavouritesConfiguration : BaseEntityConfiguration<Favourites, int>
    {
        public override void Configure(EntityTypeBuilder<Favourites> builder)
        {
            base.Configure(builder);
        }
    }
}
