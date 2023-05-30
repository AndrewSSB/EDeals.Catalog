using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Domain.Entities.TransactionDetails;
using EDeals.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<ProductCategory> ProductsCategories { get; }
        DbSet<Image> Images { get; }
        DbSet<ProductInventory> ProductsInventory { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Seller> Sellers { get; }
        DbSet<Discount> Discounts { get; }
        DbSet<UserInfo> UserInfos { get; }
        DbSet<UserAddress> UserAddresses { get; }
        DbSet<ShoppingSession> ShoppingSessions { get; }
        DbSet<CartItem> CartItems { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderedItem> OrderedItems { get; }
    }
}
