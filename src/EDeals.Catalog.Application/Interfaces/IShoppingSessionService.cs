using EDeals.Catalog.Application.Models.ShoppingSessionModels;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IShoppingSessionService
    {
        Task<ResultResponse> AddShoppingSession(AddShoppingSessionModel model);
        Task<ResultResponse<ShoppingSessionResponse>> GetShoppingSession(int id);
        Task<ResultResponse> UpdateShoppingSession(UpdateShoppingSessionModel model);
        Task<ResultResponse> DeleteShoppingSession(int id);
    }
}
