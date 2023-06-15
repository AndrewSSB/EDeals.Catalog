using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Domain.Entities.TransactionDetails;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Infrastructure.Context
{
    public class AppDbContext : DbContext//, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // should be public for interface - not ok
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductInventory> ProductsInventory { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<ShoppingSession> ShoppingSessions { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Messages> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyEntityConfigurations();

            builder.EnableSoftDeleteCascade();
        }
    }
}
