namespace EDeals.Catalog.Application.Pagination.Helpers
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public int TotalPageNumber { get; set; }
        public int CurrentPageNumber { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
