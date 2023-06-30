namespace EDeals.Catalog.Application.Models
{
    public class CreatePaymentModel
    {
        public int? ShoppingSessionId { get; set; }
        public decimal Amount { get; set; }
    }
}