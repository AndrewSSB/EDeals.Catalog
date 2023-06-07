using EDeals.Catalog.Domain.Entities.ItemEntities;
using System.Linq.Expressions;

namespace EDeals.Catalog.Application.Models.ProductModels
{
    public class ProductResponse
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }

        public Images? Images { get; set; }
        public Categories? Categories { get; set; }
        public Inventory? Inventory { get; set; }
        public ProductBrand? Brand { get; set; }
        public ProductSeller? Seller { get; set; }
        public List<Discounts>? Discounts { get; set; }

        public static Expression<Func<Product, ProductResponse>> Projection() =>
            product => new ProductResponse
            {
                Name = product.Name,
                Title = product.Title,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                Color = product.Color,
                Images = new Images
                {
                    ScrollImages = product.Images.Select(x => x.ImageUrl).ToList()
                },
                Categories = new Categories
                {

                },
                Inventory = new Inventory
                {
                    Quantity = product.ProductInventory.Quantity
                },
                Brand = new ProductBrand
                {
                    Name = product.Brand.BrandName
                },
                Seller = new ProductSeller
                {
                    Name = product.Seller.SellerName
                },
                Discounts = product.Discounts.Select(x => new Discounts
                {
                    DiscountCode = x.DiscountCode,
                    Description = x.Description,
                    DiscountName = x.DiscountName,
                    DiscountPercent = x.DiscountPercent
                }).ToList()
            };
    }

    public sealed class Images
    {
        public string? MainImage { get; set; }
        public List<string>? ScrollImages { get; set; } 
    }

    public sealed class Categories
    {
        public string? TrbSchimbat { get; set; }
    }

    public sealed class Inventory
    {
        public uint Quantity { get; set; }
    }

    public sealed class ProductBrand
    {
        public string? Name { get; set; }
    }

    public sealed class ProductSeller
    {
        public string? Name { get; set; }
    }

    public sealed class Discounts
    {
        public string? DiscountCode { get; set; }
        public string? DiscountName { get; set; }
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
