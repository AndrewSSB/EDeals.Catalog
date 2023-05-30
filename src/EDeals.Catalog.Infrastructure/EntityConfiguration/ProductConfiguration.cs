using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class ProductConfiguration : BaseEntityConfiguration<Product, Guid>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder
                .Property(p => p.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.ShortDescription)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder
                .Property(p => p.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            builder
                .Property(x => x.Color)
                .HasMaxLength(50)
                .IsRequired();


            // Config relations

            builder
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .IsRequired()
                .HasPrincipalKey(p => p.Id);

            builder
                .HasOne(pi => pi.ProductInventory)
                .WithOne(p => p.Product)
                .HasForeignKey<Product>(p => p.ProductInventoryId)
                .IsRequired();

            builder
                .HasOne(b => b.Brand)
                .WithOne(p => p.Product)
                .HasForeignKey<Product>(p => p.BrandId)
                .IsRequired();

            builder
                .HasOne(b => b.Seller)
                .WithOne(p => p.Product)
                .HasForeignKey<Product>(p => p.SellerId)
                .IsRequired();

            builder
                .HasMany(p => p.Discounts)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId)
                .IsRequired(false);
        }
    }
}
