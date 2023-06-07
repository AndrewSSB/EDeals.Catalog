using EDeals.Catalog.Application.Models.CartItemModels;

namespace EDeals.Catalog.Application.Models.ShoppingSessionModels
{
    public class ShoppingSessionResponse
    {
        public decimal Total { get; set; }

        public List<CartItemResponse> CartItems { get; set; }
    }
}
