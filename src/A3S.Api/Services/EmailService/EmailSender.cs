using A3S.Api.Services.RegisterUserService;
using A3S.Core.ConfigOptions;
using A3S.Core.Models.Content;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace A3S.Api.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSetting)
        {
            _emailSettings = emailSetting.Value;
        }

        public async Task SendEmail(EmailData emailData)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress(emailData.ToName ?? string.Empty, emailData.ToEmail));
            message.Subject = emailData.Subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = emailData.Content,
                HtmlBody = emailData.Content
            };
            
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort ?? 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new InvalidOperationException("Failed to send email", ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
