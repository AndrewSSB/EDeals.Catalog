using EDeals.Catalog.Application.Interfaces.Email;
using EDeals.Catalog.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EDeals.Catalog.Application.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IBaseEmailService _sendinBlueService;
        private readonly ApplicationSettings _appSettings;

        public EmailService(ILogger<EmailService> logger, IBaseEmailService sendinBlueService, IOptions<ApplicationSettings> appSettings)
        {
            _sendinBlueService = sendinBlueService;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task SendConfirmationEmail(string to, string name, CancellationToken cancellationToken)
        {
            var filename = "wwwroot/EmailTemplates/ConfirmationEmail.html";

            var template = await LoadTemplate(filename, cancellationToken);

            if (template == null) return;

            template = template
                .Replace("{name}", name)
                .Replace("{image_url}", _appSettings.LogoUrl);

            await _sendinBlueService.SendEmailUsingApi(to, "Confirmare plata", template);
            //await _sendinBlueService.SendEmailUsingSmtp(to, "Verification code", template);
        }

        private async Task<string?> LoadTemplate(string filename, CancellationToken cancellationToken = default)
        {
            //var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //if (string.IsNullOrEmpty(basePath))
            //{
            //    return null;
            //}

            try
            {
                //var webRootPath = Path.Combine(basePath, "wwwroot", filename);

                _logger.LogInformation("File path {filename}", filename);

                var templateSupport = await File.ReadAllTextAsync(filename, cancellationToken);

                return templateSupport;
            }
            catch
            {
                _logger.LogError("Can't load the template, check if the path is correct");
                return null;
            }
        }
    }
}
