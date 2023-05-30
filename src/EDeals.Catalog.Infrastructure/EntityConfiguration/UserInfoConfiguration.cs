using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Infrastructure.EntityConfiguration.BaseConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDeals.Catalog.Infrastructure.EntityConfiguration
{
    public class UserInfoConfiguration : BaseEntityConfiguration<UserInfo, int>
    {
        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(50);

            builder
                .Property(x => x.UserName)
                .HasMaxLength(50);

            builder
                .Property(x => x.Email)
                .HasMaxLength(80);

            builder
                .Property(x => x.PhoneNumber)
                .HasMaxLength(15);

            builder
                .HasMany(ui => ui.UsersAddresses)
                .WithOne(ua => ua.UserInfo)
                .HasForeignKey(ua => ua.UserInfoId)
                .IsRequired(false);
        }
    }
}
