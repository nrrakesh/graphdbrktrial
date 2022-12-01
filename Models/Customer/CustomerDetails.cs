using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class CustomerDetails
    { 

        [Required, Key]
        public string CustRefID { get; set; }//Customer Reference ID.

        [Required]
        public string LangType { get; set; }

        public string Gender { get; set; }

        [Required]
        public string CreatedDate { get; set; }
        [Required]
        public string UpdateDate { get; set; }
        [Required]
        public string DOB { get; set; }

        public long IsInterestOpted { get; set; }

        public string CustStatus { get; set; }

        public long isExisting { get; set; }

        public long IscustomerQuestinere { get; set; }

        public string JLGCustomerId { get; set; }
    }
}
