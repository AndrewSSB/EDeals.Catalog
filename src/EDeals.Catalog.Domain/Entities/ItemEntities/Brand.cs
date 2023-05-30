using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class Brand : BaseEntity<int>
    {
        public string BrandName { get; set; }
        public virtual Product Product { get; set; }
    }
}
