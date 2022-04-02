using System.Threading.Tasks;

namespace SSO.Services.Interface
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
