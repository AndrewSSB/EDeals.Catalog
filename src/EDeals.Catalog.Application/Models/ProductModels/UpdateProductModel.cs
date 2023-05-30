namespace EDeals.Catalog.Application.Models.ProductModels
{
    public class UpdateProductModel
    {
        public Guid ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public decimal? Price { get; set; }
        public string? Color { get; set; }

        //public int MainImageId { get; set; }
        //public virtual Image MainImage { get; set; }

        //public int ProductCategoryId { get; set; }
        //public virtual ProductCategory ProductCategory { get; set; }

        //public int ProductInventoryId { get; set; }
        //public virtual ProductInventory ProductInventory { get; set; }

        //public int BrandId { get; set; }
        //public virtual Brand Brand { get; set; }

        //public int SellerId { get; set; }
        //public virtual Seller Seller { get; set; }

        //public virtual ICollection<Image> Images { get; set; }
        //public virtual ICollection<Discount> Discounts { get; set; }
    }
}
