namespace EDeals.Catalog.Application.Models.CategoryModels
{
    public class AddCategoryModel
    {
        public string Name { get; set; } 
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
