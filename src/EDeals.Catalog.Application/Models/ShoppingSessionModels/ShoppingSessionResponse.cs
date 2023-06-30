using EDeals.Catalog.Application.Models.CartItemModels;

namespace EDeals.Catalog.Application.Models.ShoppingSessionModels
{
    public class ShoppingSessionResponse
    {
        public int ShoppingSessionId { get; set; }
        public decimal Total { get; set; }
        public decimal? TotalWithDiscount { get; set; }
        public decimal? TransportPrice { get; set; }
        public decimal? DiscountPercent { get; set; }   

        public List<CartItemResponse> CartItems { get; set; }
    }
}
