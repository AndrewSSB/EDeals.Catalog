namespace EDeals.Catalog.Application.Pagination.Filters
{
    public class ProductsFilters : PaginationCriteria
    {
        public string? ProductName { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
