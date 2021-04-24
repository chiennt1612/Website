using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SSO.Extensions;
using SSO.Helpers;
using SSO.Services.Interface;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SSO.Services
{
    public class TwilioSmsService : ISmsSender
    {
        private readonly ILogger<TwilioSmsService> _logger;
        private readonly SMSoptions _optionsAccessor;
        private readonly IDecryptorProvider _decryptor;
        public TwilioSmsService(ILogger<TwilioSmsService> logger, SMSoptions optionsAccessor, IDecryptorProvider decryptor)
        {
            _optionsAccessor = optionsAccessor;
            _logger = logger;
            _decryptor = decryptor;
        }
        public Task SendSmsAsync(string number, string message)
        {
            try
            {
                // Plug in your SMS service here to send a text message.

                // Please check MessageServices_twilio.cs or MessageServices_ASPSMS.cs
                // for implementation details.
                _logger.LogInformation($"Sending number: {number}, message: {message}");
                TwilioClient.Init(_decryptor.Decrypt(_optionsAccessor.SMSAccountIdentification), _decryptor.Decrypt(_optionsAccessor.SMSAccountPassword));

                var Msg = MessageResource.Create(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(_optionsAccessor.SMSAccountFrom),
                    to: new Twilio.Types.PhoneNumber(number.Mobile())
                );
                _logger.LogInformation($"Sending number: {number}, message: {message} is {Msg.Sid}");
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception {ex} during sending email: {number}, subject: {message}");
                throw;
            }
        }
    }
}
