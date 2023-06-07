using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.SellerModels;
using EDeals.Catalog.Application.Pagination.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SellerResponse>> GetSeller(int id) =>
            ControllerExtension.Map(await _sellerService.GetSeller(id));
        

        [HttpPost]
        public async Task<ActionResult> AddSeller(AddSellerModel model) =>
            ControllerExtension.Map(await _sellerService.AddSeller(model));


        [HttpPut]
        public async Task<ActionResult> UpdateSeller(UpdateSellerModel model) =>
            ControllerExtension.Map(await _sellerService.UpdateSeller(model));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSeller(int id) =>
            ControllerExtension.Map(await _sellerService.DeleteSeller(id));
    }
}
