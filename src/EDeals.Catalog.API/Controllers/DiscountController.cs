﻿using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.DiscountModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost]
        public async Task<ActionResult> AddDiscount(AddDiscountModel model) =>
            ControllerExtension.Map(await _discountService.AddDiscount(model));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscount(int id) =>
            ControllerExtension.Map(await _discountService.DeleteDiscount(id));
        

        [HttpGet("{discountCode}")]
        public async Task<ActionResult<DiscountResponse>> GetDiscount(string discountCode) =>
            ControllerExtension.Map(await _discountService.GetDiscount(discountCode));

        [HttpGet("all")]
        public async Task<ActionResult<PagedResult<DiscountResponse>>> GetDiscounts(int start, int limit) =>
            ControllerExtension.Map(await _discountService.GetDiscounts(new DiscountsFilters { Start = start, Limit = limit}));

        [HttpPut]
        public async Task<ActionResult> UpdateDiscount(UpdateDiscountModel model) =>
            ControllerExtension.Map(await _discountService.UpdateDiscount(model));
        
        [HttpPost("trigger")]
        public async Task<ActionResult> ActivateOrDezactivate(ActivateOrDezactivateDiscountModel model) =>
            ControllerExtension.Map(await _discountService.ActivateOrDezactivateDiscount(model));
        
        [HttpPost("apply-discount")]
        public async Task<ActionResult> ApplyDiscount(ApplyDiscountModel model) =>
            ControllerExtension.Map(await _discountService.ApplyDiscount(model));
    }
}
