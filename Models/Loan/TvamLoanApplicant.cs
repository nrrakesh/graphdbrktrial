using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamLoanApplicant
    {
        [Required, Key]
        public string LoanApplicationId { get; set; }
        public string LoanApplicantName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string CreatedDateTime { get; set; }
        public string UpdatedDateTime { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationQualification { get; set; }
    }
}
