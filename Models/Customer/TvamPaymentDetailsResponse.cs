using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class TvamPaymentDetailsResponse
    {
        [Required, Key]
        public string PaymentId { get; set; }
        [Required, Key]
        public string TvamCustId { get; set; }
        [Required]
        public string PaymentRefNo { get; set; }
        public string BankName { get; set; }
        public double Amount { get; set; }
        public string PGMode { get; set; }
        public string Status { get; set; }
        public string TxnDateTime { get; set; }
        public string ServiceName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string PaymentGateway { get; set; }
        public string TvamOrderRefNo { get; set; }
        public string CardCategory { get; set; }
        public long IsRefunded { get; set; }
        public string Refunddatetime { get; set; }

    }
}
