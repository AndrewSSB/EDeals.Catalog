using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IStripeService
    {
        Task<ResultResponse<CreatePaymentResponse>> CreatePayment(CreatePaymentModel model);
    }
}
