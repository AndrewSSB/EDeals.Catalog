using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Models.SellerModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface ISellerService
    {
        Task<ResultResponse> AddSeller(AddSellerModel model);
        Task<ResultResponse<SellerResponse>> GetSeller(int id);
        Task<ResultResponse> UpdateSeller(UpdateSellerModel model);
        Task<ResultResponse> DeleteSeller(int id);
    }
}
