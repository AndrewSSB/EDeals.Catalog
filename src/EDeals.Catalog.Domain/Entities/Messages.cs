using EDeals.Catalog.Domain.Common;

namespace EDeals.Catalog.Domain.Entities
{
    public class Messages : BaseEntity<int>
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string ChannelId { get; set; }
        public string Message { get; set; }
        public bool IsPositive { get; set; }
    }
}
