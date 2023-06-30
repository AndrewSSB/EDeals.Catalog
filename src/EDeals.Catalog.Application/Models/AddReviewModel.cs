using EDeals.Catalog.Domain.Entities;

namespace EDeals.Catalog.Application.Models
{
    public class AddReviewModel
    {
        public float? Rating { get; set; }
        public string? Title { get; set; }
        public string Comment { get; set; }
        public Guid ProductId { get; set; }
    }
}
