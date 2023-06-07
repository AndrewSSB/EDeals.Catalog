using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.BrandModels;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandResponse>> GetBrand(int id) =>
            ControllerExtension.Map(await _brandService.GetBrand(id));

        [Produces("application/json")]
        [HttpPost()]
        public async Task<ActionResult<BrandResponse>> AddBrand(AddBrandModel model) =>
            ControllerExtension.Map(await _brandService.AddBrand(model));

        [Produces("application/json")]
        [HttpPut()]
        public async Task<ActionResult<BrandResponse>> UpdateBrand(UpdateBrandModel model) =>
            ControllerExtension.Map(await _brandService.UpdateBrand(model));

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BrandResponse>> DeleteBrand(int id) =>
            ControllerExtension.Map(await _brandService.DeleteBrand(id));
    }
}
