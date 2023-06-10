using EDeals.Catalog.Domain.Entities.ItemEntities;
using System.Linq.Expressions;

namespace EDeals.Catalog.Application.Models.CategoryModels
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
        public CategoryResponse? ParentCategory { get; set; }

        public List<CategoryResponse>? SubCategories { get; set; }

        public static Expression<Func<ProductCategory, CategoryResponse>> Projection() =>
            category => new CategoryResponse
            {
                CategoryId = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ParentCategoryId = category.ParentCategory != null && !category.ParentCategory.IsDeleted ? category.ParentCategoryId : null,
                ParentCategory = category.ParentCategory != null ? new CategoryResponse
                {
                    CategoryName = category.ParentCategory.CategoryName,
                    Description = category.ParentCategory.Description,
                } : null,
                SubCategories = category.SubCategories.Select(subCategory => new CategoryResponse
                {
                    CategoryName = subCategory.CategoryName,
                    Description = subCategory.Description,
                    ParentCategoryId = subCategory.ParentCategoryId
                }).ToList()
            };
    }
}
