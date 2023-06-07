namespace EDeals.Catalog.Application.Models.DiscountModels
{
    public class AddDiscountModel
    {
        public string DiscountCode { get; set; }
        public string DiscountName { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
