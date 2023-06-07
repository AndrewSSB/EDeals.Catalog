using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CartItemModels;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [Produces("application/json")]
        [HttpGet("all")]
        public async Task<ActionResult<PagedResult<CartItemResponse>>> GetCartItems(int start, int limit) =>
            ControllerExtension.Map(await _cartItemService.GetCartItems(new CartItemsFilters { Limit = limit, Start = start }));

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemResponse>> GetCartItem(int id) =>
            ControllerExtension.Map(await _cartItemService.GetCartItem(id));

        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> AddCartItem(AddCartItemModel model) =>
            ControllerExtension.Map(await _cartItemService.AddCartItem(model));

        [Produces("application/json")]
        [HttpPut]
        public async Task<ActionResult> UpdateCartItem(UpdateCartItemModel model) =>
            ControllerExtension.Map(await _cartItemService.UpdateCartItem(model));

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCartItem(int id) =>
            ControllerExtension.Map(await _cartItemService.DeleteCartItem(id));
    }
}