using System;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Models
{
    public class InvoiceResultModel
    {
        public bool IsInvoice { get; set; } = false;

        public string vpc_TxnResponseCode { get; set; }
        public string vpc_Message { get; set; }
        public string vpc_OrderInfo { get; set; }

        public string onePayID { get; set; }
        public string invoiceNo { get; set; }
        public string customerCode { get; set; }
        public int invoiceAmount { get; set; }
        public InvoiceResult invoiceResult { get; set; }
        public PayResult payResult { get; set; }

        public EntityFramework.Web.Entities.Contact order { get; set; }
    }

    public class InvoiceFindModel : SearchDateModel
    {
        //public string CustomerCode { get; set; }
        public int? PaymentStatus { get; set; }
    }

    public class SearchDateModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
