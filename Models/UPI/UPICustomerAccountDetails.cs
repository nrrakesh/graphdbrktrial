using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.UPI
{
    public class UPICustomerAccountDetails
    {
        [Required, Key]
        public string TvamCustRefId { get; set; }
        public string Fvaddr { get; set; }
    }
}
