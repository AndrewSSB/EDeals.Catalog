namespace EDeals.Catalog.Application.Models
{
    public class UserAddressesResponse
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string? AddressAditionally { get; set; }
    }
}
