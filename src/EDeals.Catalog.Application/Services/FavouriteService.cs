using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.Favourites;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class FavouriteService : Result, IFavouriteService
    {
        private readonly IGenericRepository<Favourites> _favourites;
        private readonly IGenericRepository<Product> _products;
        private readonly ICustomExecutionContext _executionContext;

        public FavouriteService(IGenericRepository<Favourites> favourites, ICustomExecutionContext executionContext, IGenericRepository<Product> products)
        {
            _favourites = favourites;
            _executionContext = executionContext;
            _products = products;
        }

        public async Task<ResultResponse> AddFavourite(AddFavouriteModel model)
        {
            await _favourites.AddAsync(new Favourites
            {
                UserId = _executionContext.UserId,
                ProductId = model.ProductId
            });

            return Ok();
        }

        public async Task<ResultResponse> DeleteFavourite(Guid productId)
        {
            var favouriteItem = await _favourites.
                ListAllAsQueryable()
                .Where(x => x.ProductId == productId)
                .FirstOrDefaultAsync();
            
            if (favouriteItem != null)
            {
                await _favourites.DeleteAsync<int>(favouriteItem);
            }

            return Ok();
        }

        public async Task<ResultResponse<List<ProductResponse>>> GetFavourites()
        {
            var favouriteProducts = await _favourites
                .ListAllAsQueryable()
                .Where(x => x.UserId == _executionContext.UserId)
                .Select(x => x.ProductId)
                .ToListAsync();

            var products = await _products
                .ListAllAsQueryable()
                .Where(x => favouriteProducts.Contains(x.Id))
                .Include(x => x.ProductCategory)
                    .Include(x => x.ProductInventory)
                    .Include(x => x.ProductDiscounts)
                        .ThenInclude(x => x.Discount)
                    .Include(x => x.Brand)
                    .Include(x => x.Seller)
                    .Include(x => x.Images)
                .Select(ProductResponse.Projection())
                .ToListAsync();

            return Ok(products);
        }
    }
}
