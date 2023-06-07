using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.BrandModels;
using EDeals.Catalog.Application.Pagination.Filters;
using EDeals.Catalog.Application.Pagination.Helpers;
using EDeals.Catalog.Domain.Common.ErrorMessages;
using EDeals.Catalog.Domain.Common.GenericResponses.BaseResponses;
using EDeals.Catalog.Domain.Common.GenericResponses.ServiceResponse;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Application.Services
{
    public class BrandService : Result, IBrandService
    {
        private readonly IGenericRepository<Brand> _brandRepository;

        public BrandService(IGenericRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<ResultResponse> AddBrand(AddBrandModel model)
        {
            if (string.IsNullOrEmpty(model.BrandName))
            {
                return BadRequest(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Brand name must not be null"));
            }

            await _brandRepository.AddAsync(new Brand
            {
                BrandName = model.BrandName
            });

            return Ok();
        }

        public async Task<ResultResponse> DeleteBrand(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);

            if (brand != null)
            {
                await _brandRepository.DeleteAsync<int>(brand);
            }

            return Ok();
        }

        public async Task<ResultResponse<BrandResponse>> GetBrand(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);

            if (brand == null)
            {
                return BadRequest<BrandResponse>(new ResponseError(ErrorCodes.InternalServer, ResponseErrorSeverity.Error, "Brand not found"));
            }

            return Ok(new BrandResponse
            {
                BrandName = brand.BrandName
            });
        }

        public Task<ResultResponse<PagedResult<BrandResponse>>> GetBrands(BrandsFilters filters)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse> UpdateBrand(UpdateBrandModel model)
        {
            var brand = await _brandRepository.GetByIdAsync(model.BrandId);

            if (brand == null) return Ok();

            if (!string.IsNullOrEmpty(model.BrandName))
            {
                brand.BrandName = model.BrandName;
            }

            await _brandRepository.UpdateAsync(brand);

            return Ok();
        }
    }
}
