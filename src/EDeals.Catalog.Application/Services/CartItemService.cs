using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CartItemModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IGenericRepository<Seller> _sellerRepository;
        private readonly ICustomExecutionContext _executionContext;

        public CartItemService(IGenericRepository<Seller> sellerRepository, ICustomExecutionContext executionContext)
        {
            _sellerRepository = sellerRepository;
            _executionContext = executionContext;
        }

        public Task<ResultResponse> AddCartItem(AddCartItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> DeleteCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse<CartItemResponse>> GetCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse<PagedResult<CartItemResponse>>> GetCartItems(CartItemsFilters filters)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> UpdateCartItem(UpdateCartItemModel model)
        {
            throw new NotImplementedException();
        }
    }
}
