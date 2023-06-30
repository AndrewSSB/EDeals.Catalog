using EDeals.Catalog.Application.ApplicationExtensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CategoryModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class CategoryService : Result, ICategoryService
    {
        private readonly IGenericRepository<ProductCategory> _productCategory;

        public CategoryService(IGenericRepository<ProductCategory> productCategory)
        {
            _productCategory = productCategory;
        }

        public async Task<ResultResponse> AddCategory(AddCategoryModel model)
        {
            var parentCategory = await _productCategory.GetByIdAsync(model.ParentCategoryId);

            var category = new ProductCategory
            {
                CategoryName = "",
                Description = "",
                ParentCategory = parentCategory
            };

            await _productCategory.AddAsync(category);

            return Ok();
        }

        public async Task<ResultResponse<CategoryResponse>> GetCategory(int id)
        {
            var category = await _productCategory
                .ListAllAsQueryable()
                    .Include(x => x.ParentCategory)
                    .Include(x => x.SubCategories)
                .Where(x => x.Id == id)
                .Select(CategoryResponse.Projection())
                .FirstOrDefaultAsync();

            if (category == null) return BadRequest<CategoryResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, ""));

            return Ok(category);
        }

        public async Task<ResultResponse<List<CategoryResponse>>> GetCategories()
        {
            var categories = await _productCategory
                .ListAllAsQueryable()
                    .Include(x => x.ParentCategory)
                    .Include(x => x.SubCategories)
                .Select(CategoryResponse.Categories())
                .ToListAsync();

            var categoryDictionary = categories.ToDictionary(x => x.CategoryId);
            foreach (var category in categories)
            {
                if (category.ParentCategoryId.HasValue &&
                    categoryDictionary.TryGetValue(category.ParentCategoryId.Value, out var parentCategory))
                {
                    parentCategory.SubCategories ??= new List<CategoryResponse>();
                    parentCategory.SubCategories.Add(category);
                }
            }

            var topLevelCategories = categories.Where(x => !x.ParentCategoryId.HasValue).ToList();

            return Ok(topLevelCategories);
        }

        public async Task<ResultResponse> UpdateCategory(UpdateCategoryModel model)
        {
            var category = await _productCategory.GetByIdAsync(model.CategoryId);

            if (category == null) return Ok();

            if (!string.IsNullOrEmpty(model.CategoryName))
            {
                category.CategoryName = model.CategoryName;
            }
            
            if (!string.IsNullOrEmpty(model.Description))
            {
                category.Description = model.Description;
            }

            if (model.ParentCategoryId != null)
            {
                var parentCategory = await _productCategory.GetByIdAsync(model.ParentCategoryId);

                category.ParentCategory = parentCategory ?? category.ParentCategory;
            }

            await _productCategory.UpdateAsync(category);

            return Ok();
        }

        public async Task<ResultResponse> DeleteCategory(int id)
        {
            var entity = await _productCategory.GetByIdAsync(id);

            if (entity != null)
            {
                await _productCategory.DeleteAsync<int>(entity);
            }

            return Ok();
        }
    }
}
