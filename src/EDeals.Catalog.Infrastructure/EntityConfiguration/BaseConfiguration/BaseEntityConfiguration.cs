using EDeals.Catalog.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration
{
    internal abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<Guid>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(entity => !entity.IsDeleted);
        }
    }
}
