namespace Yummy.WebAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
