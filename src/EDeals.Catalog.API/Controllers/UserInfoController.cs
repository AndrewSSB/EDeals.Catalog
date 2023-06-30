using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserInfo(AddUserInfoModel model)
        {
            await _userInfoService.AddUserInfo(model);

            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult<List<UserAddressesResponse>>> Get() =>
            ControllerExtension.Map(await _userInfoService.GetUserAddresses());
        
        [HttpPost("save-address")]
        public async Task<IActionResult> SaveAddress(SaveUserAddress model)
        {
            await _userInfoService.SaveAddress(model);

            return Ok();
        }
    }
}
