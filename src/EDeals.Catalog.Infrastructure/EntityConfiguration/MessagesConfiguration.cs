using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class MessagesConfiguration : BaseEntityConfiguration<Messages, int>
    {
        public override void Configure(EntityTypeBuilder<Messages> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Message)
                .HasMaxLength(500);

            builder.Property(x => x.IsPositive)
                .HasDefaultValue(true);
        }
    }
}
