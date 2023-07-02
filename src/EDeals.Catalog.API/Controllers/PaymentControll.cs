using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_27407608e6ce2c1bade9fb8a2ab3ffb11770a7e5176ab3a2acc3472a9ea4c1bc";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                return ControllerExtension.Map(await _stripeService.UpdateOrderPaymentStatus(stripeEvent));
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
