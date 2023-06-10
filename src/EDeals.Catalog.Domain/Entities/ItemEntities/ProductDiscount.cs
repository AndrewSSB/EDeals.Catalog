using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class ProductDiscount : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
