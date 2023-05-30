using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class Image : BaseEntity<int>
    {
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
