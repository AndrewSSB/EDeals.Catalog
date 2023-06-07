using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Application.Models.CartItemModels
{
    public class UpdateCartItemModel
    {
        public int Quantity { get; set; }
        public int CartItemId { get; set; }

        public Guid ProductId { get; set; }
    }
}
