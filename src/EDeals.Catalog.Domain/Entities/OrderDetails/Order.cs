using EDeals.Catalog.Domain.Common;
using EDeals.Catalog.Domain.Enums;

namespace EDeals.Catalog.Domain.Entities.TransactionDetails
{
    public class Order : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public decimal Total { get; set; }
        public decimal TransportPrice { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public bool IsDraft { get; set; }
        public string? PaymentIntentId { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string? AddressAditionally { get; set; }

        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
    }
}
