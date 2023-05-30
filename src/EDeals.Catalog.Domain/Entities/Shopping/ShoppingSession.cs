using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.Shopping
{
    public class ShoppingSession : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
