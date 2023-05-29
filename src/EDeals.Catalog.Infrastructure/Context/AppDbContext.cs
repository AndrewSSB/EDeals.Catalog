using Microsoft.EntityFrameworkCore;

namespace EDeals.Catalog.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyEntityConfigurations();

            builder.EnableSoftDeleteCascade();
        }
    }
}
