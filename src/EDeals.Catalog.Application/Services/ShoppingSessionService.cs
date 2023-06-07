using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CartItemModels;
using EDeals.Catalog.Application.Models.ShoppingSessionModels;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.Application.Services
{
    public class ShoppingSessionService : Result, IShoppingSessionService
    {
        private readonly IGenericRepository<ShoppingSession> _shoppingRepository;
        private readonly ICustomExecutionContext _executionContext;

        public ShoppingSessionService(IGenericRepository<ShoppingSession> shoppingRepository, ICustomExecutionContext executionContext)
        {
            _shoppingRepository = shoppingRepository;
            _executionContext = executionContext;
        }

        public Task<ResultResponse> AddShoppingSession(AddShoppingSessionModel model)
        {
            throw new NotImplementedException();
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
                .GetByIdAsync(id);

            if (shoppingSession == null)
            {
                return BadRequest<ShoppingSessionResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Shopping session does not exists"));
            }

            return Ok(new ShoppingSessionResponse
            {
                Total = shoppingSession.Total,
                CartItems = shoppingSession.CartItems.Select(x => new CartItemResponse
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ShoppingSessionId = x.ShoppingSessionId,
                    ProductPrice = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList()
            });
        }

        public Task<ResultResponse> UpdateShoppingSession(UpdateShoppingSessionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
