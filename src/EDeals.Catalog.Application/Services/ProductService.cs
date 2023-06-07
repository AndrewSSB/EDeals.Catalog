using AutoMapper;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class ProductService : Result, IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        private readonly IGenericRepository<Seller> _sellerRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IUploadPhotosService _uploadPhotos;
        
        //private readonly IAppDbContext _context;

        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository,
            IMapper mapper,
            IGenericRepository<ProductCategory> categoryRepository,
            IGenericRepository<Brand> brandRepository,
            IGenericRepository<Seller> sellerRepository,
            IUploadPhotosService uploadPhotos)//, IAppDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _sellerRepository = sellerRepository;
            _uploadPhotos = uploadPhotos;
            //_context = context;
        }

        public async Task<ResultResponse<PagedResult<ProductResponse>>> GetHomePageAsync()
        {
            var productsQueryable = _productRepository
                .ListAllAsQueryable()
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductInventory)
                    .Include(x => x.Discounts)
                    .Include(x => x.Brand)
                    .Include(x => x.Seller)
                    .Include(x => x.Images)
                .Select(ProductResponse.Projection());

            return Ok(await productsQueryable.MapToPagedResultAsync(new ProductsFilters()));
        }

        public async Task<ResultResponse> AddProduct(AddProductModel model)
        {
            var category = await _categoryRepository.GetByIdAsync(model.CategoryId);

            if (category == null)
            {
                return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Invalid category"));
            }

            var seller = await _sellerRepository.GetByIdAsync(model.SellerId);

            if (seller == null)
            {
                return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Invalid seller"));
            }

            var brand = await _brandRepository.GetByIdAsync(model.BrandId);
            
            if (brand == null)
            {
                return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Invalid brand"));
            }

            var product = new Product
            {
                Name = model.Name,
                Title = model.Title,
                Description = model.Description,
                ShortDescription = model.ShortDescription,
                StockKeepingUnit = Guid.NewGuid(),
                Price = model.Price,
                Color = model.Color,
                ProductCategory = category,
                ProductInventory = new ProductInventory
                {
                    Quantity = model.Quantity.HasValue ? Convert.ToUInt32(model.Quantity) : 1
                },
                Seller = seller,
                Brand = brand,
            };

            await _productRepository.AddAsync(product);

            await _uploadPhotos.UploadPhotos(model.Images, product.Id, product.Name);

            return Ok(model);
        }

        public async Task<ResultResponse<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _productRepository
                .ListAllAsQueryable()
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductInventory)
                    .Include(x => x.Discounts)
                    .Include(x => x.Brand)
                    .Include(x => x.Seller)
                    .Include(x => x.Images)
                .Where(x => x.Id == id)
                .Select(ProductResponse.Projection())
                .FirstOrDefaultAsync();

            return product == null ? BadRequest(new ProductResponse()) : Ok(product) ;
        }

        public async Task<ResultResponse<PagedResult<ProductResponse>>> GetProducts(ProductsFilters filters)
        {
            filters.Start = filters.Start < 0 || filters.Start > filters.Limit ? 0 : filters.Start;
            filters.Limit = filters.Limit <= 0 || filters.Limit > 60 ? 60 : filters.Limit;

            var productsQueryable = _productRepository
                .ListAllAsQueryable()
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductInventory)
                    .Include(x => x.Discounts)
                    .Include(x => x.Brand)
                    .Include(x => x.Seller)
                    .Include(x => x.Images)
                .Select(ProductResponse.Projection());

            return Ok(await productsQueryable.MapToPagedResultAsync(filters));
        }

        public async Task<ResultResponse> UpdateProduct(UpdateProductModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.ProductId);

            if (product == null)
            {
                return BadRequest(new ResultResponse());
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                product.Name = model.Name;
            }
            
            if (!string.IsNullOrEmpty(model.Title))
            {
                product.Title = model.Title;
            }
            
            if (!string.IsNullOrEmpty(model.Description))
            {
                product.Description = model.Description;
            }
            
            if (!string.IsNullOrEmpty(model.ShortDescription))
            {
                product.ShortDescription = model.ShortDescription;
            }
            
            if (model.Price.HasValue)
            {
                product.Price = model.Price.Value;
            }
            
            if (!string.IsNullOrEmpty(model.Color))
            {
                product.Color = model.Color;
            }

            await _productRepository.UpdateAsync(product);

            return Ok();
        }
        
        public async Task<ResultResponse> DeleteProduct(Guid id)
        {
            var entity = await _productRepository.GetByIdAsync(id);

            if (entity != null)
            {
                await _productRepository.DeleteAsync<Guid>(entity);
            }

            return Ok();
        }
    }
}
