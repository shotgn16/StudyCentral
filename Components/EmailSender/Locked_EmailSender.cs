using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace StudyCentralV2.Components.EmailSender
{
    public class Locked_EmailSender
    {
        private readonly ILogger _logger;
        public Locked_EmailSender() => _logger = configureServices.Logger;

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(configureServices._configuration["MailSettings:SMTP-Server"], 587)
            {
                Credentials = new NetworkCredential(configureServices._configuration["MailSettings:Username"], configureServices._configuration["MailSettings:Password"])
            };

            return client.SendMailAsync(
                new MailMessage(from: "admin@studycentral.uk", to: email, subject, message));
        }
    }
}
