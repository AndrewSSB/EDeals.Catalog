using EDeals.Catalog.Domain.Common;
using EDeals.Catalog.Domain.Enums;

namespace EDeals.Catalog.Domain.Entities.TransactionDetails
{
    public class Order : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public decimal Total { get; set; }
        public PaymentTypes PaymentType { get; set; }


        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
    }
}
