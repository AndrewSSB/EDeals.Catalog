using Azure.Core;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Interfaces.Email;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Domain.Entities.TransactionDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace EDeals.Catalog.Application.Services
{
    public class StripeService : Result, IStripeService
    {
        private readonly IGenericRepository<ShoppingSession> _shoppingSession;
        private readonly IGenericRepository<Domain.Entities.ItemEntities.Product> _product;
        private readonly IGenericRepository<CartItem> _cartItem;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<UserInfo> _userInfo;
        private readonly ILogger<StripeService> _logger;
        private readonly IEmailService _emailService;

        public StripeService(IGenericRepository<ShoppingSession> shoppingSession, IGenericRepository<CartItem> cartItem, IGenericRepository<Domain.Entities.ItemEntities.Product> product, IGenericRepository<Order> orderRepository, ILogger<StripeService> logger, IEmailService emailService, IGenericRepository<UserInfo> userInfo)
        {
            _shoppingSession = shoppingSession;
            _cartItem = cartItem;
            _product = product;
            _orderRepository = orderRepository;
            _logger = logger;
            _emailService = emailService;
            _userInfo = userInfo;
        }

        public async Task<ResultResponse<CreatePaymentResponse>> CreatePayment(CreatePaymentModel model)
        {
            var paymentIntentService = new PaymentIntentService();

            var amount = model.ShoppingSessionId.HasValue ? await CalculateAmount(model.ShoppingSessionId.Value) : (long)model.Amount * 100;

            if (amount is null or 0)
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

            var order = await _orderRepository.GetByIdAsync(model.OrderId);

            if (order is not null)
            {
                order.PaymentIntentId = paymentIntent.Id;
                await _orderRepository.UpdateAsync(order);
            }

            return Ok(new CreatePaymentResponse { ClientSecret = paymentIntent.ClientSecret});
        }

        public async Task<ResultResponse> UpdateOrderPaymentStatus(Event stripeEvent)
        {
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentCreated:
                    {
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                        var order = await _orderRepository.ListAllAsQueryable().Where(x => x.PaymentIntentId == paymentIntent.Id).FirstOrDefaultAsync();
                        if (order is not null)
                        {
                            _logger.LogInformation("The payment with id {paymentId} was created.", paymentIntent.Id);
                            order.PaymentStatus = Domain.Enums.PaymentStatus.PAYMENT_INTENT_CREATED;
                            await _orderRepository.UpdateAsync(order);
                        }else
                        {
                            _logger.LogError("There is no order with id {paymentIntent.Id}", paymentIntent?.Id);
                        }
                        
                        return Ok();
                    }
                
                case Events.PaymentIntentSucceeded:
                    {
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                        var order = await _orderRepository.ListAllAsQueryable().Where(x => x.PaymentIntentId == paymentIntent.Id).FirstOrDefaultAsync();
                        if (order is not null)
                        {
                            _logger.LogInformation("A successful payment for {paymentIntent.Amount} was made.", paymentIntent.Amount);
                            order.PaymentStatus = Domain.Enums.PaymentStatus.PAYMENT_INTENT_SUCCEEDED;
                            await _orderRepository.UpdateAsync(order);
                        }
                        else
                        {
                            _logger.LogError("There is no order with id {paymentIntent.Id}", paymentIntent?.Id);
                        }

                        return Ok();
                    }
                
                case Events.ChargeSucceeded:
                    {
                        var charge = stripeEvent.Data.Object as Charge;
                        var order = await _orderRepository.ListAllAsQueryable().Where(x => x.PaymentIntentId == charge.PaymentIntentId).FirstOrDefaultAsync();
                        if (order is not null)
                        {
                            _logger.LogInformation("A successful payment for {paymentIntent.Amount} was made.", charge.Amount);
                            order.PaymentStatus = Domain.Enums.PaymentStatus.CHARGED_SUCCEEDED;
                            await _orderRepository.UpdateAsync(order);
                            SendEmail(order.UserId);
                        }
                        else
                        {
                            _logger.LogError("There is no order with id {paymentIntent.Id}", charge?.Id);
                        }
                        
                        return Ok();    
                    }
                case Events.PaymentIntentPaymentFailed:
                case Events.PaymentIntentCanceled:
                case Events.ChargeFailed:
                    {
                        var paymentId = stripeEvent.Data.Object as PaymentIntent;
                        var order = await _orderRepository.ListAllAsQueryable().Where(x => x.PaymentIntentId == paymentId.Id).FirstOrDefaultAsync();
                        if (order is not null)
                        {
                            _logger.LogInformation("A successful payment for {paymentIntent.Amount} was made.", paymentId.Amount);
                            order.PaymentStatus = Domain.Enums.PaymentStatus.FAILED;
                            await _orderRepository.UpdateAsync(order);
                        }
                        else
                        {
                            _logger.LogError("There is no order with id {paymentIntent.Id}", paymentId?.Id);
                        }
                        return BadRequest();
                    }
                default:
                    {
                        _logger.LogError("Unhandled event type: {0}", stripeEvent.Type);
                        return BadRequest();
                    }
            }
        }

        private async Task<long?> CalculateAmount(int sessionId)
        {
            var shoppingSession = await _shoppingSession.GetByIdAsync(sessionId);

            if (shoppingSession == null) return null;

            return (long) (shoppingSession.Total * 100); // 10.00 lei => 1000 bani
        }

        private async Task SendEmail(Guid userId)
        {
            var userInfo = await _userInfo.ListAllAsQueryable().Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (userInfo == null) return;

            await _emailService.SendConfirmationEmail(userInfo.Email, userInfo.FirstName);
        }
    }
}
