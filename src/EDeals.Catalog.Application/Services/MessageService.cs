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

        public MessageService(IGenericRepository<Messages> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Messages?> CheckForChannel(Expression<Func<Messages, bool>> action) => 
            await _messageRepository.ListAllAsQueryable().FirstOrDefaultAsync(action);

        public async Task CreateMessage(string sender, string channelId, string message)
        {
            await _messageRepository.AddAsync(new Messages
            {
                Username = sender,
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
                    Username = x.Username
                })
                .ToListAsync();
        }
    }
}
