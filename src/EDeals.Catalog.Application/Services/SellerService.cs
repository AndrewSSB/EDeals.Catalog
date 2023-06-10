using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.SellerModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class SellerService : Result, ISellerService
    {
        private readonly IGenericRepository<Seller> _sellerRepository;
        private readonly ICustomExecutionContext _executionContext;
        public SellerService(IGenericRepository<Seller> sellerRepository, ICustomExecutionContext executionContext)
        {
            _sellerRepository = sellerRepository;
            _executionContext = executionContext;
        }

        public async Task<ResultResponse> AddSeller(AddSellerModel model)
        {
            await _sellerRepository.AddAsync(new Seller
            {
                SellerName = model.SellerName,
                UserId = _executionContext.UserId,
            });

            return Ok();
        }

        public async Task<ResultResponse> DeleteSeller(int id)
        {
            var seller = await _sellerRepository.GetByIdAsync(id);

            seller ??= await _sellerRepository
                .ListAllAsQueryable()
                .Where(x => x.UserId == _executionContext.UserId)
                .FirstOrDefaultAsync();

            if (seller != null)
            {
                await _sellerRepository.DeleteAsync<int>(seller);
            }

            return Ok();
        }

        public async Task<ResultResponse<SellerResponse>> GetSeller(int id)
        {
            var seller = await _sellerRepository
                .ListAllAsQueryable()
                .Where(x => x.UserId == _executionContext.UserId && x.Id == id)
                .Select(x => new SellerResponse
                {
                    SellerId = x.Id,
                    SellerName = x.SellerName 
                })
                .FirstOrDefaultAsync();

            if (seller == null)
            {
                return BadRequest<SellerResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Seller does not exists"));
            }

            return Ok(seller);
        }

        public async Task<ResultResponse> UpdateSeller(UpdateSellerModel model)
        {
            var seller = await _sellerRepository
                .ListAllAsQueryable()
                .Where(x => x.UserId == _executionContext.UserId)
                .FirstOrDefaultAsync();

            if (seller == null) return Ok();

            if (!string.IsNullOrEmpty(model.SellerName))
            {
                seller.SellerName = model.SellerName;
            }

            await _sellerRepository.UpdateAsync(seller);

            return Ok();
        }
    }
}
