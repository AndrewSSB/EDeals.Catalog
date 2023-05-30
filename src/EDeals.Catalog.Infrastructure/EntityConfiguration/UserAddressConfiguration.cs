using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class UserAddressConfiguration : BaseEntityConfiguration<UserAddress, int>
    {
        public override void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            base.Configure(builder);

            builder
                .Property(ua => ua.Country)
                .HasMaxLength(50);

            builder
                .Property(ua => ua.City)
                .HasMaxLength(50);

            builder
                .Property(ua => ua.Address)
                .HasMaxLength(150);

            builder
                .Property(ua => ua.AddressAditionally)
                .HasMaxLength(100);

            builder
                .Property(ua => ua.Country)
                .HasMaxLength(50);

            builder
                .Property(ua => ua.PostalCode)
                .HasMaxLength(10);

            builder
                .Property(ua => ua.Region)
                .HasMaxLength(30);
        }
    }
}
