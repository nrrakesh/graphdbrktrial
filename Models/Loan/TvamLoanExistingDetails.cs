using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanExistingDetails
    {
        [Required]
        public string LoanApplicationId { get; set; }
        [Required]
        public string ExistingLoanId { get; set; }
        public string FinancialInstitution { get; set; }
        public string LoanFacilityType { get; set; }
        public double LoanAmount { get; set; }
        public double InterestRate { get; set; }
        public double EMI { get; set; }
        public double PrincipalOutstanding { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }

    }
}
