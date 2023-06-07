using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.ShoppingSessionModels;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;

namespace EDeals.Catalog.Application.Services
{
    public class ShoppingSessionService : IShoppingSessionService
    {
        private readonly IGenericRepository<Seller> _sellerRepository;
        private readonly ICustomExecutionContext _executionContext;

        public ShoppingSessionService(IGenericRepository<Seller> sellerRepository, ICustomExecutionContext executionContext)
        {
            _sellerRepository = sellerRepository;
            _executionContext = executionContext;
        }

        public Task<ResultResponse> AddShoppingSession(AddShoppingSessionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> DeleteShoppingSession(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse<ShoppingSessionResponse>> GetShoppingSession(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> UpdateShoppingSession(UpdateShoppingSessionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
