using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Loan
{
    public class TvamBLAddressDetail
    {
        [Required]
        public string LoanApplicationId { get; set; }
        public string ResidentTypeId { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string DurationAtCurrentResidenceSinceMonth { get; set; }
        public string City { get; set; }
        public long IsPermanentAddressSame { get; set; }
        public string DurationAtCurrentResidenceSinceYear { get; set; }
        public string District { get; set; }
        public string Id { get; set; }
        public string AddressType { get; set; }
        //public string Taluka { get; set; }
        public string CurrentResidenceOwnerShip { get; set; }
        //public string ResidenceType { get; set; }
        public string AddressFor { get; set; }

    }
}
