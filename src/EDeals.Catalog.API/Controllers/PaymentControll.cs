using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentControll : Controller
    {
        private readonly IStripeService _stripeService;

        public PaymentControll(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }
        
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment(CreatePaymentModel model) =>
            ControllerExtension.Map(await _stripeService.CreatePayment(model));
    }
}
