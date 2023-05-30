using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class ProductInventory : BaseEntity<int>
    {
        public uint Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
