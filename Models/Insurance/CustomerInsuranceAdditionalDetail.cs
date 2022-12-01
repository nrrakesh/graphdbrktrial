using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Insurance
{
    public class CustomerInsuranceAdditionalDetail
    {
        [Required, Key]
        public string InsuranceID { get; set; }
        public string PinCode { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        [Required]
        public string CreatedDate { get; set; }
    }
}
