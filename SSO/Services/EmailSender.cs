﻿using Microsoft.Extensions.Logging;
using SSO.Services.Interface;
using System.Threading.Tasks;

namespace SSO.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation($"Sending email: {email}, subject: {subject}, message: {message}");
            return Task.CompletedTask;
        }
    }
}
