using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class Seller : BaseEntity<int>
    {
        public string SellerName { get; set; }
        public virtual Product Product { get; set; }
    }
}
