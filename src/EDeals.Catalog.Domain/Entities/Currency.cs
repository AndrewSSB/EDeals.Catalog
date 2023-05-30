using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities
{
    public class Currency : BaseEntity<int>
    {
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string Symbol { get; set; }
    }
}
