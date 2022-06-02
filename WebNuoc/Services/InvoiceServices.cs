using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        private ILogger<InvoiceServices> ilogger;
        private IConfiguration configuration;
        private InvoiceConfig invoiceConfig;

        public InvoiceServices(ILogger<InvoiceServices> ilogger, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ilogger = ilogger;
            invoiceConfig = this.configuration.GetSection(nameof(InvoiceConfig)).Get<InvoiceConfig>();
        }
        public async Task<PayResult> CheckPayInvoice(CheckPayInput inv)
        {
            PayResult a = default;
            try
            {
                a = await webRequest<PayResult, CheckPayInput>(invoiceConfig.APIFunctions[2], inv);
                ilogger.LogInformation($"Check pay status invoice {inv.OnePayID} is result {a.PayStatus}");
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Check pay status invoice {inv.OnePayID} is error {ex.Message}");
            }
            return a;
        }

        public async Task<InvoiceResult> GetInvoice(InvoiceInput inv)
        {
            InvoiceResult a = default;
            try
            {
                a = await webRequest<InvoiceResult, InvoiceInput>(invoiceConfig.APIFunctions[0], inv);
                ilogger.LogInformation($"GetInvoice {inv.CustomerCode} is result {a.ItemsData.CustomerName}");
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetInvoice {inv.CustomerCode} is error {ex.Message}");
            }
            return a;
        }

        public async Task<InvoiceAllResult> GetInvoiceAll(InvoiceAllInput inv)
        {
            InvoiceAllResult a = default;
            try
            {
                a = await webRequest<InvoiceAllResult, InvoiceAllInput>(invoiceConfig.APIFunctions[4], inv);
                ilogger.LogInformation($"GetInvoiceHistory {inv.CustomerCode} is result {a.Message}");
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetInvoiceHistory {inv.CustomerCode} is error {ex.Message}");
            }
            return a;
        }

        public async Task<PayResult> PayInvoice(PayInput inv)
        {
            PayResult a = default;
            try
            {
                a = await webRequest<PayResult, PayInput>(invoiceConfig.APIFunctions[1], inv);
                ilogger.LogInformation($"PayInvoice {inv.CustomerCode} is result {a.Message}");
            }
            catch (Exception ex)
            {
                ilogger.LogError($"PayInvoice {inv.CustomerCode} is error {ex.Message}");
            }
            return a;
        }

        public async Task<UndoPayResult> UndoPayInvoice(InvoiceInput inv)
        {
            UndoPayResult a = default;
            try
            {
                a = await webRequest<UndoPayResult, InvoiceInput>(invoiceConfig.APIFunctions[2], inv);
                ilogger.LogInformation($"UndoPayInvoice {inv.CustomerCode} is result {a.Message}");
            }
            catch (Exception ex)
            {
                ilogger.LogError($"UndoPayInvoice {inv.CustomerCode} is error {ex.Message}");
            }
            return a;
        }

        #region Private
        private void InitiateSSLTrust()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                   new RemoteCertificateValidationCallback(
                        delegate
                        { return true; }
                    );
            }
            catch
            {

            }
        }
        private async Task<T> webRequest<T, T1>(string functionName, T1 pzData)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(this.invoiceConfig.APIUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic " + this.invoiceConfig.APIToken);
            httpWebRequest.Headers.Add("FunctionName", functionName);

            if (pzData != null)
            {
                using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
                {
                    string json = JsonConvert.SerializeObject(pzData);
                    await streamWriter.WriteAsync(json);
                    await streamWriter.FlushAsync();
                    streamWriter.Close();
                }
            }
            InitiateSSLTrust();//bypass SSL
            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<T>(result);
        }
        #endregion
    }
}
