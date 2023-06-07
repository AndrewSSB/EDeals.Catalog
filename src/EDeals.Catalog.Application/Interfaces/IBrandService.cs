using EDeals.Catalog.Application.Models.BrandModels;
using EDeals.Catalog.Application.Models.CategoryModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IBrandService
    {
        Task<ResultResponse> AddBrand(AddBrandModel model);
        Task<ResultResponse<BrandResponse>> GetBrand(int id);
        Task<ResultResponse<PagedResult<BrandResponse>>> GetBrands(BrandsFilters filters);
        Task<ResultResponse> UpdateBrand(UpdateBrandModel model);
        Task<ResultResponse> DeleteBrand(int id);
    }
}
