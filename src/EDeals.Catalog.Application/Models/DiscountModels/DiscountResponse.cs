using EDeals.Catalog.Domain.Entities.ItemEntities;
using System.Linq.Expressions;

namespace EDeals.Catalog.Application.Models.DiscountModels
{
    public class DiscountResponse
    {
        public string? DiscountCode { get; set; }
        public string? DiscountName { get; set; }
        public string? Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool Active { get; set; }

        public static Expression<Func<Discount, DiscountResponse>> Projection() =>
            discount => new DiscountResponse
            {
                Description = discount.Description,
                DiscountCode = discount.DiscountCode,
                Active = discount.Active,
                DiscountName = discount.DiscountName,
                DiscountPercent = discount.DiscountPercent
            };
    }
}
