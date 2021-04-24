using Microsoft.Extensions.Logging;
using SSO.Services.Interface;
using System.Threading.Tasks;

namespace SSO.Services
{
    public class SmsSender : ISmsSender
    {
        private readonly ILogger<SmsSender> _logger;
        public SmsSender(ILogger<SmsSender> logger)
        {
            _logger = logger;
        }
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.

            // Please check MessageServices_twilio.cs or MessageServices_ASPSMS.cs
            // for implementation details.
            _logger.LogInformation($"Sending number: {number}, message: {message}");
            return Task.FromResult(0);
        }
    }
}
