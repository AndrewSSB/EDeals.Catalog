using EDeals.Catalog.API.Extensions;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace EDeals.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentControll : Controller
    {
        private readonly IStripeService _stripeService;
        private readonly StripeSettings _stripeSettings;

        public PaymentControll(IStripeService stripeService, IOptions<StripeSettings> stripeSettings)
        {
            _stripeService = stripeService;
            _stripeSettings = stripeSettings.Value;
        }

        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment(CreatePaymentModel model) =>
            ControllerExtension.Map(await _stripeService.CreatePayment(model));

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, _stripeSettings.WebHookSecret);

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
