namespace EDeals.Catalog.Application.Models.DiscountModels
{
    public class ApplyDiscountModel
    {
        public int DiscountId { get; set; }
        public Guid ProductId { get; set; }
    }
}
