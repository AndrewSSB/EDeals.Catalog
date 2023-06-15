using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("{channelId}")]
        public async Task<ActionResult<List<MessagesResponse>>> GetMessages(string channelId) =>
             Ok(await _service.GetMessage(channelId));
    }
}
