using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Application.Models.CategoryModels
{
    public class UpdateCategoryModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
