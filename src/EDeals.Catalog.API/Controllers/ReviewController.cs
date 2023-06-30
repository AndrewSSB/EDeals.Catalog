using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IProductReviewService _productReviewService;

        public ReviewController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        [HttpPost("review")]
        public async Task<ActionResult> AddReview(AddReviewModel model) =>
            ControllerExtension.Map(await _productReviewService.AddReview(model));

        [HttpPost("question")]
        public async Task<ActionResult> AddQuestion(AddReviewModel model) =>
            ControllerExtension.Map(await _productReviewService.AddQuestion(model));

        [HttpGet("reviews")]
        public async Task<ActionResult<List<ReviewsResponse>>> GetReviews() =>
            ControllerExtension.Map(await _productReviewService.GetReviews());
    }
}
