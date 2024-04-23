using ARC_InternetBanking.Core.Application.Dtos.Email;
using ARC_InternetBanking.Core.Domain.Settings;

namespace ARC_InternetBanking.Infrastructure.Shared.Service
{
    public interface IEmailService
    {
        MailSettings _mailSettings { get; }

        Task SendAsync(EmailRequest request);
    }
}