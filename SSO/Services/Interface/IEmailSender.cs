using System.Threading.Tasks;

namespace SSO.Services.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
