using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.BrandModels;
using EDeals.Catalog.Application.Models.Favourites;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly IFavouriteService _service;

        public FavoritesController(IFavouriteService service)
        {
            _service = service;
        }

        [Produces("application/json")]
        [HttpPost()]
        public async Task<ActionResult> AddFavorites(AddFavouriteModel model) =>
            ControllerExtension.Map(await _service.AddFavourite(model));
        
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFavorites(Guid id) =>
            ControllerExtension.Map(await _service.DeleteFavourite(id));
        
        [Produces("application/json")]
        [HttpGet()]
        public async Task<ActionResult<List<ProductResponse>>> GetFavorites() =>
            ControllerExtension.Map(await _service.GetFavourites());
    }
}
