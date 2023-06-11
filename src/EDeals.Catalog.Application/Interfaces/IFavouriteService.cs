using EDeals.Catalog.Application.Models.Favourites;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IFavouriteService
    {
        Task<ResultResponse> AddFavourite(AddFavouriteModel model);
        Task<ResultResponse> DeleteFavourite(int id);
        Task<ResultResponse<List<ProductResponse>>> GetFavourites();
    }
}
