using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanApplication
    {
        [Required, Key]
        public string LoanApplicationId { get; set; }   
        public string LoanTypeId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        public string LoanApplicationStatus { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
    }
}
