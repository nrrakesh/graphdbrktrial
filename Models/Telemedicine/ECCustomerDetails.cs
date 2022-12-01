using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Telemedicine
{
    public class ECCustomerDetails
    {
        [Required]
        public string Id { get; set; }
        [Required, Key]
        public string TvamCustid { get; set; }
        public string ChiCustomerid { get; set; }
        public string FamilyMemberid { get; set; }
        public long IsPrimaryCustomer { get; set; }
        public string DateOfBirth { get; set; }
        public string PolicyStatus { get; set; }
        public string Gender { get; set; }
        public string CurrentPolicyStatus { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Is_EConsultation { get; set; }
        public string CoverType { get; set; }
        public string PlanName { get; set; }
        public string PolicyCommencementDt { get; set; }
        public string PolicyMaturityDt { get; set; }
        public string IsBookConsultationAllowed { get; set; }
    }
}
