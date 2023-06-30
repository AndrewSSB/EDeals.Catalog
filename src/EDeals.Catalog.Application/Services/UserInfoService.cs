using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Application.Models.ShoppingSessionModels;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class UserInfoService : Result, IUserInfoService
    {
        private readonly IGenericRepository<UserInfo> _userInfo;
        private readonly IGenericRepository<UserAddress> _userAddress;
        private readonly ICustomExecutionContext _customExecutionContext;

        public UserInfoService(IGenericRepository<UserInfo> userInfo, ICustomExecutionContext customExecutionContext, IGenericRepository<UserAddress> userAddress)
        {
            _userInfo = userInfo;
            _customExecutionContext = customExecutionContext;
            _userAddress = userAddress;
        }

        public async Task AddUserInfo(AddUserInfoModel model)
        {
            await _userInfo.AddAsync(new UserInfo
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = _customExecutionContext.UserId
            });
        }

        public async Task<ResultResponse> SaveAddress(SaveUserAddress model)
        {
            var user = await _userInfo
                .ListAllAsQueryable()
                .Where(x => x.UserId == _customExecutionContext.UserId)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Discount code does not exists"));
            }

            await _userAddress.AddAsync(new UserAddress
            {
                UserInfo = user,
                Address = model.Address,
                AddressAditionally = model.AddressAditionally,
                City = model.City,
                Country = model.Country,
                PostalCode = model.PostalCode,
                Region = model.Region,
            });

            return Ok();
        }

        public async Task<ResultResponse<List<UserAddressesResponse>>> GetUserAddresses()
        {
            var userInfo = await _userInfo.ListAllAsQueryable().Where(x => x.UserId == _customExecutionContext.UserId).FirstOrDefaultAsync();

            if (userInfo is null)
            {
                return BadRequest< List < UserAddressesResponse >> (new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Discount code does not exists"));
            }

            return Ok(await _userAddress
                .ListAllAsQueryable()
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.UserInfoId == userInfo.Id).Select(x => new UserAddressesResponse
            {
                Address = x.Address,
                AddressAditionally = x.AddressAditionally,
                City = x.City,
                Country = x.Country,
                PostalCode = x.PostalCode,
                Region = x.Region,
            }).ToListAsync());
        }
    }
}
