using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using EDeals.Catalog.Domain.Entities.TransactionDetails;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class OrderService : Result, IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<ShoppingSession> _shoppingSession;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<UserAddress> _userAddressRepository;
        private readonly ICustomExecutionContext _customExecutionContext;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<Product> productRepository, ICustomExecutionContext customExecutionContext, IGenericRepository<ShoppingSession> shoppingSession, IGenericRepository<UserAddress> userAddressRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customExecutionContext = customExecutionContext;
            _shoppingSession = shoppingSession;
            _userAddressRepository = userAddressRepository;
        }

        public async Task<ResultResponse<int>> CreateDraftOrder(CreateDraftOrderModel model)
        {
            var shoppingSession = await _shoppingSession.ListAllAsQueryable().Include(x => x.CartItems).Where(x => x.Id == model.ShoppingSessionId).FirstOrDefaultAsync();

            if (shoppingSession != null)
            {
                var order = await _orderRepository.AddAsync(new Order
                {
                    UserId = _customExecutionContext.UserId,
                    Total = shoppingSession.Total,
                    TransportPrice = model.TransportPrice,
                    PaymentType = model.PaymentType,
                    IsDraft = true,

                    Address = model.UserAddress.Address,
                    AddressAditionally = model.UserAddress.AddressAditionally,
                    City = model.UserAddress.City,
                    Country = model.UserAddress.Country,
                    PostalCode = model.UserAddress.PostalCode,
                    Region = model.UserAddress.Region,

                    OrderedItems = shoppingSession.CartItems.Select(x => new OrderedItem
                    {
                        ProductId = x.ProductId,
                        Quantity = (uint)x.Quantity
                    }).ToList()
                });

                return Ok(order.Id);
            }

            if (!model.Total.HasValue || model.LocalShopping is null)
            {
                return BadRequest<int>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Eroare de procesare"));
            }

            var productIds = model.LocalShopping.Select(prd => prd.ProductId).ToList();

            var products = await _productRepository.ListAllAsQueryable()
                .Where(x => productIds.Contains(x.Id))
                .ToListAsync();

            var orderedItems = products.Select((product, index) => new OrderedItem
            {
                ProductId = product.Id,
                Quantity = (uint)model.LocalShopping[index].Quantity
            }).ToList();


            var orderId = await _orderRepository.AddAsync(new Order
            {
                UserId = Guid.NewGuid(),
                Total = model.Total.Value,
                TransportPrice = model.TransportPrice,
                PaymentType = model.PaymentType,
                IsDraft = true,

                Address = model.UserAddress.Address,
                AddressAditionally = model.UserAddress.AddressAditionally,
                City = model.UserAddress.City,
                Country = model.UserAddress.Country,
                PostalCode = model.UserAddress.PostalCode,
                Region = model.UserAddress.Region,

                //OrderedItems = products
            });

            return Ok(orderId.Id);
        }

        public async Task<ResultResponse> CreateOrder(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                return BadRequest<int>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Eroare de procesare"));
            }

            order.IsDraft = false;
            await _orderRepository.UpdateAsync(order);

            var shoppingSession = await _shoppingSession.ListAllAsQueryable().Where(x => x.UserId == _customExecutionContext.UserId).FirstOrDefaultAsync();

            if (shoppingSession is not null)
            {
                await _shoppingSession.DeleteAsync<int>(shoppingSession);
            }

            return Ok();
        }

        public Task<OrderResponse> GetOrder()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse<List<OrderResponse>>> GetOrders()
        {
            var orders = await _orderRepository.ListAllAsQueryable()
                .Where(x => x.UserId == _customExecutionContext.UserId && !x.IsDraft)
                .Select(x => new OrderResponse
                {
                    Address = x.Address,
                    AddressAditionally = x.AddressAditionally,
                    City = x.City,
                    Country = x.Country,
                    PaymentType = x.PaymentType.ToString(),
                    PostalCode = x.PostalCode,
                    Region = x.Region,
                    Total = x.Total,
                    TransportPrice = x.TransportPrice,
                })
                .ToListAsync();

            return Ok(orders);
        }
    }
}
