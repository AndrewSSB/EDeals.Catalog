using EDeals.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EDeals.Catalog.Application.Services
{
    public class ChatHub : Hub<IChatHub>
    {
        private static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();
        
        public async Task JoinChannel(string channelId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
        }
        
        public async Task LeaveChannel(string channelId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId.ToString());
        }

        public async Task SendMessage(string channelId, string name, string message)
        {
            await Clients.Group(channelId).ReceiveMessage(name, message);
        }
    }
}
