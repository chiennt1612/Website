using System.Collections.Generic;
namespace WebNuoc.Helpers
{
    public class RegisterConfiguration
    {
        public bool Enabled { get; set; } = true;
    }

    public enum LoginResolutionPolicy
    {
        Username = 0,
        Email = 1
    }
    public class LoginConfiguration
    {
        public LoginResolutionPolicy ResolutionPolicy { get; set; } = LoginResolutionPolicy.Username;
    }
    public class AdvancedConfiguration
    {
        public string PublicOrigin { get; set; }

        public string IssuerUri { get; set; }
    }

    public class UseCors
    {
        public bool CorsAllowAnyOrigin { get; set; }

        public string[] CorsAllowOrigins { get; set; }
    }
    public class SmtpConfiguration
    {
        public string From { get; set; }
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 587; // default smtp port
        public bool UseSSL { get; set; } = true;
    }

    public class SMSoptions
    {
        public string SMSAccountIdentification { get; set; }
        public string SMSAccountPassword { get; set; }
        public string SMSAccountFrom { get; set; }
    }

    public class AboutPage
    {
        public long AboutID { get; set; }
        public List<long> GuideID { get; set; }
    }

    public class LoggingFiles
    {
        public string FileName { get; set; }
        public long FileSizeLimitBytes { get; set; }
        public int RollingInterval { get; set; }
        public string OutputTemplate { get; set; }
    }

    public class LoggingMongoDB
    {
        public string URI { get; set; }
        public string Collection { get; set; }
    }

    public class LoggingProvider
    {
        public LoggingMongoDB LogMongoDB { get; set; }
        public LoggingFiles LogFiles { get; set; }
        public int LoggingType { get; set; }
        public LoggingProvider()
        {
            LogMongoDB = new LoggingMongoDB();
            LogFiles = new LoggingFiles();
        }
    }
}
