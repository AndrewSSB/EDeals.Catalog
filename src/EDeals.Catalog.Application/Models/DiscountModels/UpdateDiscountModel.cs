namespace EDeals.Catalog.Application.Models.DiscountModels
{
    public class UpdateDiscountModel
    {
        public int DiscountId { get; set; }
        public string? DiscountCode { get; set; }
        public string? DiscountName { get; set; }
        public string? Description { get; set; }
        public decimal? DiscountPercent { get; set; }
        public bool? Active { get; set; }
    }
}
