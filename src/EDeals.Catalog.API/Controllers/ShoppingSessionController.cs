using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.ShoppingSessionModels;
using EDeals.Catalog.Application.Pagination.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingSessionController : Controller
    {
        private readonly IShoppingSessionService _sessionService;

        public ShoppingSessionController(IShoppingSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingSessionResponse>> GetShoppingSession(int id) =>
            ControllerExtension.Map(await _sessionService.GetShoppingSession(id));
        

        [HttpPost]
        public async Task<ActionResult> AddShoppingSession(AddShoppingSessionModel model) => 
            ControllerExtension.Map(await _sessionService.AddShoppingSession(model));
        
        [HttpPost("apply-discount")]
        public async Task<ActionResult> ApplyShoppingDiscount(ApplyShoppingDiscount model) => 
            ControllerExtension.Map(await _sessionService.ApplyDiscountToShoppingSession(model));
        

        [HttpPut]
        public async Task<ActionResult> UpdateShoppingSession(UpdateShoppingSessionModel model) =>
            ControllerExtension.Map(await _sessionService.UpdateShoppingSession(model));
        

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShoppingSession(int id) =>
            ControllerExtension.Map(await _sessionService.DeleteShoppingSession(id));  
    }
}