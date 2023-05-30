using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IProductService
    {
        Task<ResultResponse<PagedResult<ProductResponse>>> GetHomePageAsync();

        Task<ResultResponse> AddProduct(AddProductModel model);
        Task<ResultResponse<ProductResponse>> GetProduct(Guid id);
        Task<ResultResponse<PagedResult<ProductResponse>>> GetProducts(ProductsFilters filters);
        Task<ResultResponse> UpdateProduct(UpdateProductModel model);
        Task<ResultResponse> DeleteProduct(Guid id);
    }
}
