using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanDetails
    {
        [Required]
        public string LoanApplicationId { get; set; }
        public string PurposeOfLoan { get; set; }
        public string LoanTypeId { get; set; }
        public double LoanAmount { get; set; }
        public double LoanDuration { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
        

    }
}
