using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class CustomerInterest
    {
        [Required, Key]
        public string CustRefID { get; set; }

        public string InterestCode { get; set; }

        public string DisplayText { get; set; }

        public long Priority { get; set; }

        public string createddate { get; set; }
    }
}
