using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Insurance
{
    public class CustomerInsurancedetails
    {
        [Required, Key]
        public string CustRefId { get; set; }
        [Required, Key]
        public string InsuranceID { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        [Required]
        public string CreatedDate { get; set; }
        public string DeclarationID { get; set; }
        public string PolicyType { get; set; }
    }
}
