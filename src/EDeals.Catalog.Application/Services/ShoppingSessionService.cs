using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CartItemModels;
using EDeals.Catalog.Application.Models.ShoppingSessionModels;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class ShoppingSessionService : Result, IShoppingSessionService
    {
        private readonly IGenericRepository<ShoppingSession> _shoppingRepository;
        private readonly IGenericRepository<Discount> _discountRepository;
        private readonly ICustomExecutionContext _executionContext;

        public ShoppingSessionService(IGenericRepository<ShoppingSession> shoppingRepository, ICustomExecutionContext executionContext, IGenericRepository<Discount> discountRepository)
        {
            _shoppingRepository = shoppingRepository;
            _executionContext = executionContext;
            _discountRepository = discountRepository;
        }

        public Task<ResultResponse> AddShoppingSession(AddShoppingSessionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse> ApplyDiscountToShoppingSession(ApplyShoppingDiscount model)
        {
            var shoppingSession = await _shoppingRepository
                .ListAllAsQueryable()
                .Where(x => x.Id == model.Id || x.UserId == _executionContext.UserId)
                .FirstOrDefaultAsync();

            if (shoppingSession == null)
            {
                return BadRequest<ShoppingSessionResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Shopping session does not exists"));
            }

            var discount = await _discountRepository.ListAllAsQueryable().Where(x => x.DiscountCode == model.DiscountCode).FirstOrDefaultAsync();

            if (discount is null)
            {
                return BadRequest<ShoppingSessionResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Discount code does not exists"));
            }

            shoppingSession.DiscountId = discount.Id;

            await _shoppingRepository.UpdateAsync(shoppingSession);

            return Ok();
        }

        public async Task<ResultResponse> DeleteShoppingSession(int id)
        {
            var shoppingSession = await _shoppingRepository.GetByIdAsync(id);
            
            if (shoppingSession != null)
            {
                await _shoppingRepository.DeleteAsync<int>(shoppingSession);
            }

            return Ok();
        }

        public async Task<ResultResponse<ShoppingSessionResponse>> GetShoppingSession(int id)
        {
            var shoppingSession = await _shoppingRepository
                .ListAllAsQueryable()
                    .Include(x => x.CartItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Images)
                .Where(x => x.Id == id || x.UserId == _executionContext.UserId)
                .FirstOrDefaultAsync();

            if (shoppingSession == null)
            {
                return BadRequest<ShoppingSessionResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Shopping session does not exists"));
            }

            var discount = await _discountRepository.GetByIdAsync(shoppingSession.DiscountId);

            return Ok(new ShoppingSessionResponse
            {
                ShoppingSessionId = shoppingSession.Id,
                Total = shoppingSession.Total,
                CartItems = shoppingSession.CartItems.Select(x => new CartItemResponse
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ShoppingSessionId = x.ShoppingSessionId,
                    ProductPrice = x.Product.Price,
                    Quantity = x.Quantity,
                    Image = x.Product.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                    Description = x.Product.ShortDescription

                }).ToList(),
                DiscountPercent = discount?.DiscountPercent,
                TotalWithDiscount = shoppingSession.Total - shoppingSession.Total * (discount?.DiscountPercent / 100 ?? 0),
                TransportPrice = 33.99M
            });
        }

        public Task<ResultResponse> UpdateShoppingSession(UpdateShoppingSessionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
