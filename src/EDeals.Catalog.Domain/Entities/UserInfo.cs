﻿using EDeals.Catalog.Domain.Common;
using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Domain.Entities
{
    public class UserInfo : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<UserAddress> UsersAddresses { get; set; }
    }
}
