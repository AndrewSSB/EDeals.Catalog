using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class ProductReviewConfiguration : BaseEntityConfiguration<ProductReview, int>
    {
        public override void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Title)
                .HasMaxLength(100);

            builder.Property(x => x.Comment)
                .HasMaxLength(500);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductReviews)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.UserInfo)
                .WithMany(x => x.ProductReviews)
                .HasForeignKey(x => x.UserInfoId);
        }
    }
}
