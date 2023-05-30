using EDeals.Catalog.Application.Pagination.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EDeals.Catalog.Application.Pagination.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PagedResult<T>> MapAllToPagedResulAsync<T, TEntity>
           (this IQueryable<TEntity> queryableEntities,
           PaginationCriteria criteria,
           Expression<Func<TEntity, T>> Projection)
        {
            var totalCount = await queryableEntities.CountAsync();

            return MapDataToPagedResult(await queryableEntities.Select(Projection).ToListAsync(), criteria, totalCount);
        }

        public static PagedResult<T> MapDataToPagedResult<T>
               (this IEnumerable<T> data,
               PaginationCriteria criteria,
               int totalCount)
        {
            return new PagedResult<T>
            {
                PageSize = criteria.Limit,
                TotalResults = totalCount,
                TotalPageNumber = totalCount % criteria.Limit == 0 ? totalCount / criteria.Limit : totalCount / criteria.Limit + 1,
                CurrentPageNumber = criteria.Start / criteria.Limit + 1,
                Data = data
            };
        }

        public static async Task<PagedResult<T>> MapToPagedResultAsync<T, DbSet>(
            this IQueryable<DbSet> queryableEntities,
            PaginationCriteria criteria,
            Expression<Func<DbSet, T>> Projection)
        {
            var totalCount = await queryableEntities.CountAsync();

            var data = await queryableEntities
                    .Skip(criteria.Start)
                    .Take(criteria.Limit)
                    .Select(Projection)
                    .ToListAsync();

            return new PagedResult<T>
            {
                PageSize = criteria.Limit,
                TotalResults = totalCount,
                TotalPageNumber = totalCount % criteria.Limit == 0 ? totalCount / criteria.Limit : totalCount / criteria.Limit + 1,
                CurrentPageNumber = criteria.Start / criteria.Limit + 1,
                Data = data
            };
        }

        public static async Task<PagedResult<T>> MapToPagedResultAsync<T>(this IQueryable<T> queryableEntities, PaginationCriteria criteria)
        {
            int totalCount = await queryableEntities.CountAsync();
            List<T> data = await queryableEntities.Skip(criteria.Start).Take(criteria.Limit).ToListAsync();
            return new PagedResult<T>
            {
                PageSize = criteria.Limit,
                TotalResults = totalCount,
                TotalPageNumber = ((totalCount % criteria.Limit == 0) ? (totalCount / criteria.Limit) : (totalCount / criteria.Limit + 1)),
                CurrentPageNumber = criteria.Start / criteria.Limit + 1,
                Data = data
            };
        }
    }
}
