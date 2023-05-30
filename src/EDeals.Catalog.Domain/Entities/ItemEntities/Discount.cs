using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class Discount : BaseEntity<int>
    {
        public string DiscountCode { get; set; }
        public string DiscountName { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool Active { get; set; }
        
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
