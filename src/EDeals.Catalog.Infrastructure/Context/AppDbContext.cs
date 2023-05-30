using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Domain.Entities.TransactionDetails;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductsCategories { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<ProductInventory> ProductsInventory { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Seller> Sellers { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<UserInfo> UserInfos { get; set; }
        DbSet<UserAddress> UserAddresses { get; set; }
        DbSet<ShoppingSession> ShoppingSessions { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderedItem> OrderedItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyEntityConfigurations();

            builder.EnableSoftDeleteCascade();
        }
    }
}
