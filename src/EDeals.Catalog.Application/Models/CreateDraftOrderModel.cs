using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Domain.Enums;

namespace EDeals.Catalog.Application.Models
{
    public class CreateDraftOrderModel
    {
        public int? ShoppingSessionId { get; set; }
        public decimal? Total { get; set; }
        public List<LocalShopping>? LocalShopping { get; set; }
        public decimal TransportPrice { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public SaveUserAddress UserAddress { get; set; }
    }

    public class LocalShopping
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
