using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.Shopping
{
    public class Favourites : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
