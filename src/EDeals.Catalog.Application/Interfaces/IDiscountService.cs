using EDeals.Catalog.Application.Models.DiscountModels;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IDiscountService
    {
        Task<ResultResponse> AddDiscount(AddDiscountModel model);
        Task<ResultResponse<DiscountResponse>> GetDiscount(int id);
        Task<ResultResponse<PagedResult<DiscountResponse>>> GetDiscounts(DiscountsFilters filters);
        Task<ResultResponse> UpdateDiscount(UpdateDiscountModel model);
        Task<ResultResponse> DeleteDiscount(int id);
        Task<ResultResponse> ActivateOrDezactivateDiscount(ActivateOrDezactivateDiscountModel model);
        Task<ResultResponse> ApplyDiscount(ApplyDiscountModel model);
    }
}
