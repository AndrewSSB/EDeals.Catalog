using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Entities;
using System.Linq.Expressions;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessage(string sender, string channelId, string message);
        Task<List<MessagesResponse>> GetMessage(string channelId);
        Task<Messages?> CheckForChannel(Expression<Func<Messages, bool>> action);
    }
}
