using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.CartItemModels;
using EDeals.Catalog.Application.Models.DiscountModels;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using EDeals.Catalog.Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class CartItemService : Result, ICartItemService
    {
        private readonly IGenericRepository<CartItem> _cartRepository;
        private readonly IGenericRepository<ShoppingSession> _shoppingRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly ICustomExecutionContext _executionContext;

        public CartItemService(IGenericRepository<CartItem> cartRepository,
            ICustomExecutionContext executionContext,
            IGenericRepository<ShoppingSession> shoppingRepository,
            IGenericRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _executionContext = executionContext;
            _shoppingRepository = shoppingRepository;
            _productRepository = productRepository;
        }

        public async Task<ResultResponse> AddCartItem(AddCartItemModel model)
        {
            var product = await _productRepository
                .GetByIdAsync(model.ProductId);

            if (product == null)
            {
                return BadRequest();
            }

            var shoppingSession = await _shoppingRepository
                .ListAllAsQueryable()
                .Where(x => x.UserId == _executionContext.UserId)
                .FirstOrDefaultAsync();


            shoppingSession ??= await _shoppingRepository.AddAsync(new ShoppingSession
            {
                UserId = _executionContext.UserId
            });

            var cartItem = await _cartRepository
                .ListAllAsQueryable()
                .IgnoreQueryFilters()
                .Where(x => x.ProductId == model.ProductId)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                cartItem = await _cartRepository.AddAsync(new CartItem
                {
                    Product = product,
                    Quantity = model.Quantity,
                    ShoppingSession = shoppingSession
                });
            
                shoppingSession.CartItems.Add(cartItem);
                shoppingSession.Total += cartItem.Product.Price * cartItem.Quantity;
            }
            else
            {
                if (cartItem.IsDeleted)
                {
                    var quantityBefore = cartItem.Quantity;
                    cartItem.IsDeleted = false;
                    cartItem.Quantity = model.Quantity;
                    var price = quantityBefore > cartItem.Quantity ? -(Math.Abs(quantityBefore - cartItem.Quantity) * cartItem.Product.Price) : (Math.Abs(quantityBefore - cartItem.Quantity) * cartItem.Product.Price);
                    var isEqual = quantityBefore == cartItem.Quantity;
                    shoppingSession!.Total += isEqual ? cartItem.Quantity * cartItem.Product.Price : price;
                } else
                {
                    var quantityBefore = cartItem.Quantity;
                    cartItem.Quantity += model.Quantity;
                    await _cartRepository.UpdateAsync(cartItem);
                    var price = quantityBefore > cartItem.Quantity ? -(Math.Abs(quantityBefore - cartItem.Quantity) * cartItem.Product.Price) : (Math.Abs(quantityBefore - cartItem.Quantity) * cartItem.Product.Price);
                    shoppingSession!.Total += price;
                }

            }

            await _shoppingRepository.UpdateAsync(shoppingSession);

            return Ok();
        }

        public async Task<ResultResponse> DeleteCartItem(int id)
        {
            var cartItem = await _cartRepository
                .ListAllAsQueryable()
                .Include(x => x.Product)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (cartItem != null)
            {
                var shoppingSession = await _shoppingRepository
                    .ListAllAsQueryable()
                    .Include(x => x.CartItems)
                    .Where(x => x.Id == cartItem.ShoppingSessionId)
                    .FirstOrDefaultAsync();

                await _cartRepository.DeleteAsync<int>(cartItem);
                
                if (shoppingSession!.CartItems.Count <= 1)
                {
                    await _shoppingRepository.DeleteAsync<int>(shoppingSession);
                }else
                {
                    shoppingSession.Total -= cartItem.Quantity * cartItem.Product.Price;
                    await _shoppingRepository.UpdateAsync(shoppingSession);
                }
            }

            return Ok();
        }

        public async Task<ResultResponse> UpdateCartItem(UpdateCartItemModel model)
        {
            var cartItem = await _cartRepository
                .ListAllAsQueryable()
                    .Include(x => x.Product)
                .Where(x => x.Id == model.CartItemId)
                .FirstOrDefaultAsync();

            if (cartItem != null)
            {
                var beforeUpdateQuantity = cartItem.Quantity;

                if (model.Quantity <= 0)
                {
                    await _cartRepository.DeleteAsync<int>(cartItem);
                    return Ok();
                }
                    
                cartItem.Quantity = model.Quantity;
                await _cartRepository.UpdateAsync(cartItem);

                var shoppingSession = await _shoppingRepository
                    .GetByIdAsync(cartItem.ShoppingSessionId);

                var price = beforeUpdateQuantity > cartItem.Quantity ? -(Math.Abs(beforeUpdateQuantity - cartItem.Quantity) * cartItem.Product.Price) : (Math.Abs(beforeUpdateQuantity - cartItem.Quantity) * cartItem.Product.Price);
                shoppingSession!.Total += price;

                await _shoppingRepository.UpdateAsync(shoppingSession);
            }

            return Ok();
        }

        public async Task<ResultResponse<PagedResult<CartItemResponse>>> GetCartItems(CartItemsFilters filters)
        {
            filters.Start = filters.Start < 0 || filters.Start > filters.Limit ? 0 : filters.Start;
            filters.Limit = filters.Limit <= 0 || filters.Limit > 60 ? 60 : filters.Limit;

            var cartItems = _cartRepository
                .ListAllAsQueryable()
                .Include(x => x.Product)
                .Include(x => x.ShoppingSession)
                .Where(x => x.ShoppingSession.UserId == _executionContext.UserId)
                .Select(x => new CartItemResponse
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ShoppingSessionId = x.ShoppingSessionId,
                    ProductPrice = x.Product.Price,
                    Quantity = x.Quantity
                });

            return Ok(await cartItems.MapToPagedResultAsync(filters));
        }

        public async Task<ResultResponse<CartItemResponse>> GetCartItem(int id)
        {
            var cartItem = await _cartRepository
                .ListAllAsQueryable()
                .Include(x => x.Product)
                .Include(x => x.ShoppingSession)
                .Where(x => x.ShoppingSession.UserId == _executionContext.UserId && x.Id == id)
                .Select(x => new CartItemResponse
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ShoppingSessionId = x.ShoppingSessionId,
                    ProductPrice = x.Product.Price,
                    Quantity = x.Quantity
                })
                .FirstOrDefaultAsync();
            
            if (cartItem == null)
            {
                return BadRequest<CartItemResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Item does not exists in the cart"));
            } 
           
            return Ok(cartItem);
        }
    }
}
