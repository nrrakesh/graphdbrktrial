using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamBLPersonalDetail
    {
        [Required, Key]
        public string LoanApplicationId { get; set; }
        [Required]
        public string LoanRefno { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Pincode { get; set; }
        public string CreatedDate { get; set; }
        public string UpdateDate { get; set; }
        [Required, Key]
        public string TvamCustomerId { get; set; }
        public string MaritalStatus { get; set; }
        public string Qualification { get; set; }
        public string Language { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }

    }
}
