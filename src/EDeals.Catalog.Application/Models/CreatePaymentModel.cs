namespace EDeals.Catalog.Application.Models
{
    public class CreatePaymentModel
    {
        public int OrderId { get;set; }
        public decimal Amount { get; set; }
        public int? ShoppingSessionId { get; set; }
    }
}