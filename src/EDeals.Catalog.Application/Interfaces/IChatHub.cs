using EDeals.Catalog.Application.Models;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IChatHub
    {
        public Task ReceiveMessage(string sender, string receiver, string message, DateTime time);
        public Task ReceiveMessages(List<MessagesResponse> messages);
        public Task UpdateQueueCountWithDelta(int countDelta);
        public Task CloseChat(Guid conversationId);
        public Task ReceiveNotification(string sender, string receiver, string type, string channelId);
    }
}
