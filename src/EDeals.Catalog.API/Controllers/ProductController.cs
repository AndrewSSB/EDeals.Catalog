using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Produces("application/json")]
        [HttpGet("homepage")]
        public async Task<ActionResult<PagedResult<ProductResponse>>> GetHomePage() =>
            ControllerExtension.Map(await _productService.GetHomePageAsync());
        
        [HttpPost("add")]
        public async Task<ActionResult<PagedResult<ProductResponse>>> AddProduct([FromForm] AddProductModel model) =>
            ControllerExtension.Map(await _productService.AddProduct(model));
        
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id) =>
            ControllerExtension.Map(await _productService.GetProduct(id));
        
        [HttpGet("products")]
        public async Task<ActionResult<PagedResult<ProductResponse>>> GetProducts(ProductsFilters filters) =>
            ControllerExtension.Map(await _productService.GetProducts(filters));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<PagedResult<ProductResponse>>> DeleteProduct(Guid id) =>
            ControllerExtension.Map(await _productService.DeleteProduct(id));

    }
}
