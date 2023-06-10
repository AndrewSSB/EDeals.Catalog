using EDeals.Catalog.Application.Models.CartItemModels;

namespace EDeals.Catalog.Application.Models.ShoppingSessionModels
{
    public class ShoppingSessionResponse
    {
        public int ShoppingSessionId { get; set; }
        public decimal Total { get; set; }

        public List<CartItemResponse> CartItems { get; set; }
    }
}
