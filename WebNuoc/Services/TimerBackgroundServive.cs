using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Paygate.OnePay;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebNuoc.Controllers;

namespace WebNuoc.Services
{
    internal class TimerBackgroundServive : IHostedService, IDisposable
    {
        private readonly ILogger<TimerBackgroundServive> _logger;
        private Timer _timer;
        public TimerBackgroundServive(ILogger<TimerBackgroundServive> _logger)
        {
            this._logger = _logger;
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(30)) ;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation($"Timed Background Service is working time start: {DateTime.Now.ToString("HH:mm:ss zzz")}");
            string result = string.Empty;
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(@"https://nuocngoctuan.com/Service/IQuery");//https://nuocngoctuan.com //https://localhost:5004
            httpWebRequest.ContentType = "text/html; charset=utf-8";
            httpWebRequest.Method = "GET";
            
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                _logger.LogInformation($"Timed Background Service is working time end: {DateTime.Now.ToString("HH:mm:ss zzz")} -> {result}");
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Timed Background Service is working time end: {DateTime.Now.ToString("HH:mm:ss zzz")} -> {ex.Message}");
            }
        }
    }
}