namespace EDeals.Catalog.Application.Models
{
    public class MessagesResponse
    {
        public DateTime Date { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public string ChannelId { get; set; }
    }
}
