using EDeals.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EDeals.Catalog.Application.Services
{
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task JoinChannel(string channelId)
        {
            var existingConnection = await DoesChannelExist(channelId);

            await Groups.AddToGroupAsync(Context.ConnectionId, existingConnection ?? channelId);
        }
        
        public async Task LeaveChannel(string channelId)
        {
            var existingConnection = await DoesChannelExist(channelId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, existingConnection ?? channelId);
        }

        public async Task SendMessage(string channelId, string name, string message)
        {
            var existingConnection = await DoesChannelExist(channelId);

            await Clients.Group(existingConnection ?? channelId).ReceiveMessage(name, message, DateTime.UtcNow);

            await _messageService.CreateMessage(name, existingConnection ?? channelId, message);
        }

        private async Task<string?> DoesChannelExist(string channelId)
        {
            if (!channelId.Contains('_')) return null;

            var firstUsername = channelId.Split("__")[0];
            var secondUsername = channelId.Split("__")[1];

            var channelIdInversed = secondUsername + "__" + firstUsername;

            var saveChannelId = await _messageService.CheckForChannel(x => x.ChannelId.Contains(channelId) || x.ChannelId.Contains(channelIdInversed));

            return saveChannelId?.ChannelId;
        }
    }
}
