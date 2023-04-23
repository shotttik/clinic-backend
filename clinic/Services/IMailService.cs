using clinic.Schemas.Auth;

namespace clinic.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
