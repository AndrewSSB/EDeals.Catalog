using Azure.Core;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using Stripe;

namespace EDeals.Catalog.Application.Services
{
    public class StripeService : Result, IStripeService
    {
        private readonly IGenericRepository<ShoppingSession> _shoppingSession;
        private readonly IGenericRepository<Domain.Entities.ItemEntities.Product> _product;
        private readonly IGenericRepository<CartItem> _cartItem;

        public StripeService(IGenericRepository<ShoppingSession> shoppingSession, IGenericRepository<CartItem> cartItem, IGenericRepository<Domain.Entities.ItemEntities.Product> product)
        {
            _shoppingSession = shoppingSession;
            _cartItem = cartItem;
            _product = product;
        }

        public async Task<ResultResponse<CreatePaymentResponse>> CreatePayment(CreatePaymentModel model)
        {
            var paymentIntentService = new PaymentIntentService();

            var amount = await CalculateAmount(model.ShoppingSessionId);

            if (amount == null)
            {
                return BadRequest<CreatePaymentResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Invalid shopping session"));
            }

            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "ron",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Ok(new CreatePaymentResponse { ClientSecret = paymentIntent.ClientSecret});
        }

        private async Task<long?> CalculateAmount(int sessionId)
        {
            var shoppingSession = await _shoppingSession.GetByIdAsync(sessionId);

            if (shoppingSession == null) return null;

            return (long) (shoppingSession.Total * 100); // 10.00 lei => 1000 bani
        }
    }
}
