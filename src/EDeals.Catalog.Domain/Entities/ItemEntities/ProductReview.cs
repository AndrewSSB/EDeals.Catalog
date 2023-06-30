using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities.ItemEntities
{
    public class ProductReview : BaseEntity<int>
    {
        public float Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool HasBoughtProduct { get; set; }
        
        public bool IsReview { get; set; }
        
        public int UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
