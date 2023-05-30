using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities
{
    public class UserAddress : BaseEntity<int>
    {
        public string Country { get; set; }
        public string City { get; set; }    
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string? AddressAditionally { get; set; }

        public int? UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; } 
    }
}
