using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EDeals.Catalog.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IGenericRepository<Messages> _messageRepository;
        private readonly IGenericRepository<UserInfo> _userInfo;
        private readonly ICustomExecutionContext _customExecutionContext;

        public MessageService(IGenericRepository<Messages> messageRepository, IGenericRepository<UserInfo> userInfo, ICustomExecutionContext customExecutionContext)
        {
            _messageRepository = messageRepository;
            _userInfo = userInfo;
            _customExecutionContext = customExecutionContext;
        }

        public async Task<Messages?> CheckForChannel(Expression<Func<Messages, bool>> action) => 
            await _messageRepository.ListAllAsQueryable().FirstOrDefaultAsync(action);

        public async Task CreateMessage(string sender, string receiver, string channelId, string message)
        {
            await _messageRepository.AddAsync(new Messages
            {
                Sender = sender,
                Receiver = receiver,
                ChannelId = channelId,
                Message = message
            });
        }

        public async Task<List<MessagesResponse>> GetMessage(string channelId)
        {
            var firstUsername = channelId.Split("__")[0];
            var secondUsername = channelId.Split("__")[1];

            var channelIdInversed = secondUsername + "__" + firstUsername;

            return await _messageRepository
                .ListAllAsQueryable()
                .Where(x => x.ChannelId.Contains(channelId) || x.ChannelId.Contains(channelIdInversed))
                .Select(x => new MessagesResponse
                {
                    ChannelId = x.ChannelId,
                    Message = x.Message,
                    Date = x.CreatedAt,
                    Sender = x.Sender,
                    Receiver = x.Receiver,
                })
                .ToListAsync();
        }
        
        public async Task<List<ConversationResponse>> GetMyConversations()
        {
            var userName = await _userInfo.ListAllAsQueryable().Where(x => x.UserId == _customExecutionContext.UserId).Select(x => x.UserName).FirstOrDefaultAsync();

            if (userName is null) return new List<ConversationResponse>();

            return await _messageRepository
                .ListAllAsQueryable()
                .Where(x => x.ChannelId.Contains(userName))
                .Select(x => new ConversationResponse
                {
                     ReceiverUsername = userName == x.ChannelId.Substring(0, x.ChannelId.IndexOf("__")) ? x.ChannelId.Substring(x.ChannelId.IndexOf("__") + 2) : x.ChannelId.Substring(0, x.ChannelId.IndexOf("__"))
                })
                .Distinct()
                .ToListAsync();
        }
    }
}
