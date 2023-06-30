using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IOrderService
    {
        Task<ResultResponse<int>> CreateDraftOrder(CreateDraftOrderModel model);
        Task<OrderResponse> GetOrder();
        Task<ResultResponse<List<OrderResponse>>> GetOrders();
        Task<ResultResponse> CreateOrder(int orderId);
    }
}
