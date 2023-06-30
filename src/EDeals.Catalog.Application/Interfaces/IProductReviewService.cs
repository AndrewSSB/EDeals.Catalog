using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IProductReviewService
    {
        Task<ResultResponse> AddReview(AddReviewModel model);
        Task<ResultResponse> AddQuestion(AddReviewModel model);
        Task<ResultResponse> GetReview();
        Task<ResultResponse<List<ReviewsResponse>>> GetReviews();
    }
}
