using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.UPI
{
    public class UPITransactionDetail
    {
        [Required, Key]
        public string TransactionId { get; set; }
        [Required, Key]
        public string TvamCustRefid { get; set; }
        public string TransactionDescription { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; }
        public string TransactionType { get; set; }
        public string PayeePaymentAddress { get; set; }
        public string PayeeIFSC { get; set; }
        public string MerchantCatCode { get; set; }
        public string PayerIFSC { get; set; }
        public string SubMerchantID { get; set; }
        public string TransactionAuthDate { get; set; }
        public string Status { get; set; }
        public string PaymentTypeCategory { get; set; }
        public string Createddate { get; set; }
        public string PaymentPurpose { get; set; }
        public string UpdatedDateTime { get; set; }
        public long IsCollectApproved { get; set; }
        //public string TxnStatustvamRefId { get; set; }
    }
}
