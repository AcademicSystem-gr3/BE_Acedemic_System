using A3S.Core.Models.Content;

namespace A3S.Api.Services.RegisterUserService
{
    public interface IEmailSender
    {
        Task SendEmail(EmailData emailData);
    }
}
