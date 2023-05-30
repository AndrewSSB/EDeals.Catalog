using EDeals.Catalog.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration
{
    public abstract class BaseEntityConfiguration<TEntity, TKeyId> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKeyId>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(entity => !entity.IsDeleted);
        }
    }
}
