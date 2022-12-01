using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Insurance
{
    public class CustomerInsurancePolicyDetails
    {
        [Required, Key]
        public string InsuranceID { get; set; }
        public string PolicyStatus { get; set; }
        public string PolicyType { get; set; }
        public string CoverType { get; set; }
        [Required]
        public string CreatedDate { get; set; }
        public string PolicyID { get; set; }
        public string CustRefID { get; set; }
        public string PolicyCreationDate { get; set; }
        public string PolicyModifyDate { get; set; }
        public string PolicyFor { get; set; }
        public string PolicyCommencementDt { get; set; }
        public string PolicyMaturityDt { get; set; }
        public string PaymentRefNo { get; set; }
        public string PolicyTerm { get; set; }
    }
}
