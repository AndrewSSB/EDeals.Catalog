using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Domain.Common;
using EDeals.Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync<TKey>(T entity)
        {
            if (typeof(BaseEntity<TKey>).IsAssignableFrom(typeof(T)))
            {
                if (entity is BaseEntity<TKey> baseEntity)
                {
                    baseEntity.IsDeleted = true;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public virtual async Task<T?> GetByIdAsync<TKey>(TKey id)
        {
            if (id == null) return null;

            var keyValues = new object[] { id };
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public IQueryable<T> ListAllAsQueryable()
        {
            return _context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
