using EDeals.Catalog.Application.Models.CategoryModels;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ResultResponse> AddCategory(AddCategoryModel model);
        Task<ResultResponse<CategoryResponse>> GetCategory(int id);
        Task<ResultResponse<List<CategoryResponse>>> GetCategories();
        Task<ResultResponse> UpdateCategory(UpdateCategoryModel model);
        Task<ResultResponse> DeleteCategory(int id);
    }
}
