using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebNuoc.Services.Interfaces
{
    public interface IInvoiceServices
    {
        Task<InvoiceResult> GetInvoice(InvoiceInput inv);
        Task<InvoiceAllResult> GetInvoiceAll(InvoiceAllInput inv);
        Task<PayResult> PayInvoice(PayInput inv);
        Task<PayResult> CheckPayInvoice(CheckPayInput inv);
        Task<UndoPayResult> UndoPayInvoice(InvoiceInput inv);
    }

    #region Object
    public class InvoiceConfig
    {
        public string APIUrl { get; set; }
        public string APIToken { get; set; }
        public List<string> APIFunctions { get; set; }
    }

    public class InvList
    {
        public string InvCode { get; set; }
        public string InvRemarks { get; set; }
        public int InvAmount { get; set; }
    }

    public class ItemsData
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public List<InvList> InvList { get; set; }
    }

    public class InvoiceAllInput
    {
        public string CustomerCode { get; set; }
        public int Page { get; set; }
    }

    public class InvoiceAllResult
    {
        public int Rowcount { get; set; }
        public ItemsDataAll ItemsData { get; set; }
        public string DataStatus { get; set; }
        public string Message { get; set; }
        public int ResponseStatus { get; set; }

        public string Keyword { get; set; } = "";
        public bool IsAgree { get; set; } = true;
    }

    public class ItemsDataAll
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WaterIndexCode { get; set; }
        public List<InvListAll> InvList { get; set; }
    }

    public class InvListAll
    {
        public int InvCode { get; set; }
        public string InvRemarks { get; set; }
        public string MaSoBiMat { get; set; }
        public string InvNumber { get; set; }
        public string InvSerial { get; set; }
        public DateTime InvDate { get; set; }
        public double TaxPer { get; set; }
        public double InvAmountWithoutTax { get; set; }
        public double InvAmount { get; set; }
        public int PaymentStatus { get; set; } // 1. Da thanh toan; 0. Chua thanh toan
    }

    public class InvoiceInput
    {
        public string CustomerCode { get; set; }
    }

    public class InvoiceResult
    {
        public ItemsData ItemsData { get; set; }
        public string DataStatus { get; set; }
        public string Message { get; set; }
        public int ResponseStatus { get; set; }

        public string Keyword { get; set; } = "";
        public bool IsAgree { get; set; } = true;
    }

    public class PayInput
    {
        public string OnePayID { get; set; }
        [Required]
        public string CustomerCode { get; set; }
        [Required]
        public string InvoiceNo { get; set; }
        [Required]
        public int InvoiceAmount { get; set; }

        public bool IsAgree { get; set; } = true;
    }

    public class PayResult
    {
        public string PayStatus { get; set; }
        public string Message { get; set; }
        public string ResponseStatus { get; set; }
    }

    public class CheckPayInput
    {
        public string OnePayID { get; set; }
    }

    public class UndoPayResult
    {
        public string UndoPayStatus { get; set; }
        public string Message { get; set; }
        public string ResponseStatus { get; set; }
    }
    #endregion
}
