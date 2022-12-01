using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.MPR
{
    public class MPRTransaction
    {
        [Required, Key]
        public string TransactionID { get; set; }
        [Required]
        public string CreatedDate { get; set; }
        public double Amount { get; set; }
        public string RechargeType { get; set; }
        public string OperatorID { get; set; }
        public string CircleID { get; set; }
        public string PlanValidity { get; set; }
        public string DataLimit { get; set; }
        public string PlanStartDate { get; set; }
        public string PlanEndDate { get; set; }
        public string CustomerID { get; set; }
        public string PaymentDate { get; set; }
        public string RechargeID { get; set; }
        public string IsRefundInitiated { get; set; }
        public string PaymentRefNo { get; set; }
        public string MobileRechargeStatus { get; set; }
        public string PlanId { get; set; }
    }
}
