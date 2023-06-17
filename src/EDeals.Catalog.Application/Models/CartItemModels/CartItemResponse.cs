namespace EDeals.Catalog.Application.Models.CartItemModels
{
    public class CartItemResponse
    {
        public int CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public int ShoppingSessionId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice{ get; set; }
        public string? Image { get; set; }
        public string? Description { get;set; }
    }
}
