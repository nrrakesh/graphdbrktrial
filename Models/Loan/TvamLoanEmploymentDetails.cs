using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanEmploymentDetails
    {
        [Required]
        public string LoanApplicationId { get; set; }
        [Required]
        public string EmployerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string IndustryType { get; set; }
        public double MonthlySalary { get; set; }
        public double ExistingMonthlyEMI { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
        public long Pincode { get; set; }
        public long CurrentWorkExperience { get; set; }
        public string Designation { get; set; }
        public string IncomeMode { get; set; }
        public long TotalWorkExperience { get; set; }

    }
}
