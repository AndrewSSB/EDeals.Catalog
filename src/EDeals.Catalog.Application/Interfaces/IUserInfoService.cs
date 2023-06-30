using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IUserInfoService
    {
        Task AddUserInfo(AddUserInfoModel model);
        Task<ResultResponse> SaveAddress(SaveUserAddress model);
        Task<ResultResponse<List<UserAddressesResponse>>> GetUserAddresses();
    }
}
