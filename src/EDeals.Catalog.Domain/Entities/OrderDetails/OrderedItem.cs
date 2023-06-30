using EDeals.Catalog.Domain.Common;
using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Domain.Entities.TransactionDetails
{
    public class OrderedItem : BaseEntity<int>
    {
        public uint Quantity { get; set; }

        public Guid ProductId { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
