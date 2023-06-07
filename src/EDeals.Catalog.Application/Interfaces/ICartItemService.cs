using EDeals.Catalog.Application.Models.CartItemModels;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<ResultResponse> AddCartItem(AddCartItemModel model);
        Task<ResultResponse<CartItemResponse>> GetCartItem(int id);
        Task<ResultResponse<PagedResult<CartItemResponse>>> GetCartItems(CartItemsFilters filters);
        Task<ResultResponse> UpdateCartItem(UpdateCartItemModel model);
        Task<ResultResponse> DeleteCartItem(int id);
    }
}
