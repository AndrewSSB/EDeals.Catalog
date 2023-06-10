using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.DiscountModels;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Models.SellerModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class DiscountService : Result, IDiscountService
    {
        private readonly IGenericRepository<Discount> _discountRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductDiscount> _productDiscountRepository;
        private readonly ICustomExecutionContext _executionContext;

        public DiscountService(ICustomExecutionContext executionContext, IGenericRepository<Discount> discountRepository, IGenericRepository<ProductDiscount> productDiscountRepository, IGenericRepository<Product> productRepository)
        {
            _executionContext = executionContext;
            _discountRepository = discountRepository;
            _productDiscountRepository = productDiscountRepository;
            _productRepository = productRepository;
        }

        public async Task<ResultResponse> AddDiscount(AddDiscountModel model)
        {
            await _discountRepository.AddAsync(new Discount
            {
                Description = model.Description,
                DiscountCode = model.DiscountCode,
                DiscountName = model.DiscountName,
                DiscountPercent = model.DiscountPercent,
            });

            return Ok();
        }

        public async Task<ResultResponse> ActivateOrDezactivateDiscount(ActivateOrDezactivateDiscountModel model)
        {
            var discount = await _discountRepository.GetByIdAsync(model.DiscountId);

            if (discount == null)
            {
                return BadRequest<DiscountResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Discount does not exists"));
            }

            discount.Active = model.IsActive;

            await _discountRepository.UpdateAsync(discount);

            return Ok();
        }

        public async Task<ResultResponse> ApplyDiscount(ApplyDiscountModel model)
        {
            var discount = await _discountRepository.GetByIdAsync(model.DiscountId);

            if (discount == null)
            {
                return BadRequest<DiscountResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Discount does not exists"));
            }

            var product = await _productRepository.GetByIdAsync(model.ProductId);
            
            if (product == null)
            {
                return BadRequest<DiscountResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Product does not exists"));
            }

            await _productDiscountRepository.AddAsync(new ProductDiscount
            {
                Discount = discount,
                Product = product
            });

            return Ok();
        }

        public async Task<ResultResponse> DeleteDiscount(int id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            
            if (discount != null)
            {
                await _discountRepository.DeleteAsync<int>(discount);
            }

            return Ok();
        }

        public async Task<ResultResponse<DiscountResponse>> GetDiscount(int id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);

            if (discount == null)
            {
                return BadRequest<DiscountResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Discount does not exists"));
            }

            var response = new DiscountResponse
            {
                Description = discount.Description,
                DiscountCode = discount.DiscountCode,
                Active = discount.Active,
                DiscountName = discount.DiscountName,
                DiscountPercent = discount.DiscountPercent
            };
            
            return Ok(response);
        }

        public async Task<ResultResponse<PagedResult<DiscountResponse>>> GetDiscounts(DiscountsFilters filters)
        {
            filters.Start = filters.Start < 0 || filters.Start > filters.Limit ? 0 : filters.Start;
            filters.Limit = filters.Limit <= 0 || filters.Limit > 60 ? 60 : filters.Limit;

            var discounts = _discountRepository
                .ListAllAsQueryable()
                .Select(DiscountResponse.Projection());

            return Ok(await discounts.MapToPagedResultAsync(filters));
        }

        public async Task<ResultResponse> UpdateDiscount(UpdateDiscountModel model)
        {
            var discount = await _discountRepository.GetByIdAsync(model.DiscountId);

            if (discount == null) return Ok();

            if (!string.IsNullOrEmpty(model.Description))
            {
                discount.Description = model.Description;
            }
            
            if (!string.IsNullOrEmpty(model.DiscountCode))
            {
                discount.DiscountCode = model.DiscountCode;
            }
            
            if (!string.IsNullOrEmpty(model.DiscountName))
            {
                discount.DiscountName = model.DiscountName;
            }
            
            if (model.DiscountPercent.HasValue)
            {
                discount.DiscountPercent = model.DiscountPercent.Value;
            }

            if (model.Active.HasValue)
            {
                discount.Active = model.Active.Value;
            }

            await _discountRepository.UpdateAsync(discount);

            return Ok();
        }
    }
}