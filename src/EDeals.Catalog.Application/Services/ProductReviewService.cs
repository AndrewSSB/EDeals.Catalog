using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Application.Models.SellerModels;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Product = EDeals.Catalog.Domain.Entities.ItemEntities.Product;

namespace EDeals.Catalog.Application.Services
{
    public class ProductReviewService : Result, IProductReviewService
    {
        private readonly IGenericRepository<ProductReview> _productReview;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        private readonly IGenericRepository<Seller> _sellerRepository;
        private readonly IGenericRepository<UserInfo> _userInfo;
        private readonly ICustomExecutionContext _customExecutionContext;

        public ProductReviewService(IGenericRepository<ProductReview> productReview, IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> categoryRepository, IGenericRepository<Seller> sellerRepository, IGenericRepository<UserInfo> userInfo, ICustomExecutionContext customExecutionContext)
        {
            _productReview = productReview;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _sellerRepository = sellerRepository;
            _userInfo = userInfo;
            _customExecutionContext = customExecutionContext;
        }

        public async Task<ResultResponse> AddQuestion(AddReviewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.ProductId);

            if (product == null) { return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Product does not exists")); }

            var user = await _userInfo
                .ListAllAsQueryable()
                .Where(x => x.UserId == _customExecutionContext.UserId)
                .FirstOrDefaultAsync();

            if (user == null) { return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "User does not exists")); }

            await _productReview.AddAsync(new ProductReview
            {
                Title = model.Title ?? "",
                Comment = model.Comment,
                Rating = 0f,
                UserInfo = user,
                Product = product,
                HasBoughtProduct = false,
                IsReview = false
            });

            return Ok();
        }

        public async Task<ResultResponse> AddReview(AddReviewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.ProductId);

            if (product == null) { return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Product does not exists")); }

            var user = await _userInfo
                .ListAllAsQueryable()
                .Where(x => x.UserId == _customExecutionContext.UserId)
                .FirstOrDefaultAsync();

            if (user == null) { return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "User does not exists")); }

            await _productReview.AddAsync(new ProductReview
            {
                Title = model.Title,
                Comment = model.Comment,
                Rating = model.Rating.HasValue ? model.Rating.Value : 0f,
                UserInfo = user,
                Product = product,
                HasBoughtProduct = false,
                IsReview = true,
            });

            return Ok();
        }

        public Task<ResultResponse> GetReview()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse<List<ReviewsResponse>>> GetReviews()
        {
            var user = await _userInfo
                .ListAllAsQueryable()
                .Where(x => x.UserId == _customExecutionContext.UserId)
                .FirstOrDefaultAsync();

            if (user == null) { return BadRequest<List<ReviewsResponse>>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "User does not exists")); }


            var reviews = await _productReview
                .ListAllAsQueryable()
                    .Include(x => x.UserInfo)
                .Where(x => x.UserInfo.UserId == _customExecutionContext.UserId)
                .Select(x => new ReviewsResponse
                {
                    Title = x.Title,
                    Comment = x.Comment,
                    Rating = x.IsReview ? x.Rating : null,
                    HasBoughtProduct = x.HasBoughtProduct,
                    IsReview = x.IsReview,
                    CreatedAt = x.CreatedAt,
                }).ToListAsync();

            return Ok(reviews);
        }
    }
}
