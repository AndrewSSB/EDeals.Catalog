namespace EDeals.Catalog.Application.Models.ProductModels
{
    public class UpdateProductModel
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public decimal? Price { get; set; }
        public string? Color { get; set; }
    }
}
