namespace EDeals.Catalog.Application.Models.CartItemModels
{
    public class AddCartItemModel
    {
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
