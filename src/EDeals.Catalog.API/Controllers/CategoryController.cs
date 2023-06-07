using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CategoryModels;
using EDeals.Catalog.Application.Pagination.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Produces("application/json")]
        [HttpPost()]
        public async Task<ActionResult> AddCategory(AddCategoryModel model) =>
            ControllerExtension.Map(await _categoryService.AddCategory(model));

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(int id) =>
            ControllerExtension.Map(await _categoryService.GetCategory(id));

        [Produces("application/json")]
        [HttpGet("all")]
        public async Task<ActionResult<PagedResult<CategoryResponse>>> GetCategories() =>
            ControllerExtension.Map(await _categoryService.GetCategories());

        [Produces("application/json")]
        [HttpPut()]
        public async Task<ActionResult<PagedResult<CategoryResponse>>> UpdateCategory(UpdateCategoryModel model) =>
            ControllerExtension.Map(await _categoryService.UpdateCategory(model));

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PagedResult<CategoryResponse>>> DeleteCategory(int id) =>
            ControllerExtension.Map(await _categoryService.DeleteCategory(id));
    }
}
