using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanPersonalInformation
    {
        [Required]
        public string LoanApplicationId { get; set; }
        public string ResidentialTypeId { get; set; }
        public string City { get; set; }
        public long Pincode { get; set; }
        public long NoOfDependents { get; set; }

    }
}
