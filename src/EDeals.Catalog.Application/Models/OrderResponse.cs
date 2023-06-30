using EDeals.Catalog.Domain.Enums;

namespace EDeals.Catalog.Application.Models
{
    public class OrderResponse
    {
        public decimal Total { get; set; }
        public decimal TransportPrice { get; set; }
        public string PaymentType { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string? AddressAditionally { get; set; }
    }
}
