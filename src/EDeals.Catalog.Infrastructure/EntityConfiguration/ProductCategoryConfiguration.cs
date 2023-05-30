using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class ProductCategoryConfiguration : BaseEntityConfiguration<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder
                .Property(p => p.CategoryName)
                .HasMaxLength(50)
                .IsRequired();
            
            builder
                .Property(p => p.Description)
                .HasMaxLength(500);

            builder
                .HasMany(p => p.Products)
                .WithOne(c => c.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId);

            builder
                .HasOne(p => p.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false);
        }
    }
}
