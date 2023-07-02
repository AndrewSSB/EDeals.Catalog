using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using Stripe;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IStripeService
    {
        Task<ResultResponse<CreatePaymentResponse>> CreatePayment(CreatePaymentModel model);
        Task<ResultResponse> UpdateOrderPaymentStatus(Event stripeEvent);
    }
}
