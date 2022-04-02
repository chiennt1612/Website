﻿using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Services
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
