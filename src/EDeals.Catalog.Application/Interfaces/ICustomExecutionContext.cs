namespace EDeals.Catalog.Application.Interfaces
{
    public interface ICustomExecutionContext
    {
        public Guid UserId { get; }
        public DateTime ExpiresAt { get; }
    }
}
