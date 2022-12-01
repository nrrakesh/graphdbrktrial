using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamBLLoanApplication
    {
        
        [Required]
        public string LoanReferenceNumber { get; set; }
        public string LoanTypeId { get; set; }
        [Required, Key]
        public string LoanApplicationId { get; set; }
        [Required, Key]
        public string TvamCustomerId { get; set; }
        public string LoanApplicationStatus { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
        public string LoanStatusDate { get; set; }
        public double LoanAmount { get; set; }
        public string NextInstalmentDate { get; set; }
        public double OutstandingBalance { get; set; }
        public double Overdue { get; set; }
        public double LastPaymentAmount { get; set; }
        public string LastPaymentDate { get; set; }
        public string BankName { get; set; }
        public string LoanStage { get; set; }
        public string LoanTenure { get; set; }
    }
}
