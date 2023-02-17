using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Web_App.Settings;

namespace Web_App.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpSettings> smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings;
        }
        public async Task Send(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from, to,
                    subject,
                    body);

            using (var emailClient = new SmtpClient(smtpSettings.Value.Host, smtpSettings.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                smtpSettings.Value.User,
                 smtpSettings.Value.Password
                 );

                await emailClient.SendMailAsync(message);
            }

        }
    }
}
