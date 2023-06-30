namespace EDeals.Catalog.Application.Models
{
    public class ReviewsResponse
    {
        public float? Rating { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool HasBoughtProduct { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsReview { get; set; }
    }
}
