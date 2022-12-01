using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Insurance
{
    public class CustomerFamilyInsuranceDetails
    {
        [Required, Key]
        public string CustRefID { get; set; }
        [Required, Key]
        public string FamilyMemID { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        [Required, Key]
        public string InsuranceID { get; set; }
        public string RelationToCustomer { get; set; }
        public string CreatedDate { get; set; }
        public string PolicyType { get; set; }

    }
}
