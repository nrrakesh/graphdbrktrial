using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class CustomerFamilyMember
    {
        [Required, Key]
        public string MemberID { get; set; }//Member Reference ID.

        [Required, Key]
        public string CustRefID { get; set; }//Customer Reference ID to which the member belong.

        public string Gender { get; set; }

        [Required]
        public string DOB { get; set; }

        public string RelationShipType { get; set; }

        [Required]
        public string CreatedDate { get; set; }

        [Required]
        public string UpdateDate { get; set; }

        public string Status { get; set; }
    }
}
