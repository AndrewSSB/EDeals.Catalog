using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class ProductCategory : BaseEntity<int>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }

        public virtual ICollection<ProductCategory> SubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
