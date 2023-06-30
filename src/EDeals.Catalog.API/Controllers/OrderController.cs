using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("draft")]
        public async Task<ActionResult<int>> CreateDraftOrder(CreateDraftOrderModel model) =>
            ControllerExtension.Map(await _orderService.CreateDraftOrder(model));

        [HttpPost("{id}")]
        public async Task<ActionResult<int>> CreateDraftOrder(int id) =>
            ControllerExtension.Map(await _orderService.CreateOrder(id));
        
        [HttpGet()]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders() =>
            ControllerExtension.Map(await _orderService.GetOrders());
    }
}
