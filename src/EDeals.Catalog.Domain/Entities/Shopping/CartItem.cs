using EDeals.Catalog.Domain.Common;
using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Domain.Entities.Shopping
{
    public class CartItem : BaseEntity<int>
    {
        public int Quantity { get; set; }
        
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int ShoppingSessionId { get; set; }
        public virtual ShoppingSession ShoppingSession { get; set; }
    }
}
