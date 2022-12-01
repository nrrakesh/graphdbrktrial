using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanAddress
    {
        [Required, Key]
        public string LoanApplicationId { get; set; }
        [Required]
        public string AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressType { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
        public long Pincode { get; set; }
        public string District { get; set; }

    }
}
