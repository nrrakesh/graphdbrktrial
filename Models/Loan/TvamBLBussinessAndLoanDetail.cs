using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamBLBussinessAndLoanDetail
    {
        [Required, Key]
        public string LoanApplicationId { get; set; }
        [Required]
        public string LoanRefno { get; set; }
        public string LoanPurposeId { get; set; }
        public double LoanAmount { get; set; }
        public string BussinessTypeId { get; set; }
        public string CurrentBussinessSinceMonth { get; set; }
        public string CurrentBussinessSinceYear { get; set; }
        public long TotalBussinessWorkExperienceMonth { get; set; }
        public long TotalBussinessWorkExperienceYear { get; set; }
        public string CreatedDate { get; set; }
        public string UpdateDate { get; set; }
        [Required, Key]
        public string TvamCustomerId { get; set; }
        public double MonthlySales { get; set; }
        public string CibilScore { get; set; }
        public string OtherBusinessType { get; set; }
        public long LoanTenure { get; set; }
    }
}
