namespace EDeals.Catalog.Application.Interfaces.Email
{
    public interface IBaseEmailService
    {
        Task SendEmailUsingApi(string to, string subject, string htmlContent);
        Task SendEmailUsingSmtp(string to, string subject, string htmlContent, CancellationToken cancellationToken = default);
    }
}
