namespace EDeals.Catalog.Application.Interfaces
{
    public interface IChatHub
    {
        public Task ReceiveMessage(string name, string message);
        public Task UpdateQueueCountWithDelta(int countDelta);
        public Task CloseChat(Guid conversationId);
    }
}
