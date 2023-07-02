namespace EDeals.Catalog.Application.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendConfirmationEmail(string to, string name, CancellationToken cancellationToken = default);
    }
}
