using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Yummy.WebAPI.Configurations;

namespace Yummy.WebAPI.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress("Kullanıcı", email));
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { TextBody = message };
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailSettings.Server, _mailSettings.Port, false);
                await client.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
