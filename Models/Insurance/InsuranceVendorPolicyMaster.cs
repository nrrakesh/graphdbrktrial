using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Insurance
{
    public class InsuranceVendorPolicyMaster
    {
        public string VendorID { get; set; }
        public string InsuranceType { get; set; }
        public string VendorCode { get; set; }
        public string CoverType { get; set; }
        public string PolicyID { get; set; }
        public string PolicyName { get; set; }
        public double SumInsured { get; set; }
        public string InsuranceTag { get; set; }
        public string FeatureId { get; set; }
        public string AmountPerMember { get; set; }
        public double PremiumAmount { get; set; }
        public double MaxMembers { get; set; }
        public double ChildrenMinAge { get; set; }
        public double ChildrenMaxAge { get; set; }
        public string PolicyTenureType { get; set; }
        public string PolicyTenureValue { get; set; }
        public string CreatedDate { get; set; }
        public string PolicyRegion { get; set; }
        public long MinMembers { get; set; }
        public string ProductCode { get; set; }
        public string AllowedRelationships { get; set; }
        public string AllowedCombinations { get; set; }
        public double AdultMaxAge { get; set; }
        public double AdultMinAge { get; set; }
    }
}
