using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class CustomerAddressDetails
    {
        [Required, Key]
        public string AddRefID { get; set; }//Uneque Reference number.

        [Required]
        public string CustRefID { get; set; }//Customer Reference number

        [Required]
        public string AddressType { get; set; }//Address type Home/Office/other

        public string LandMark { get; set; }

        public string City { get; set; }
                
        public string State { get; set; }
            
        public string PinCode { get; set; }
        
        public string District { get; set; }
                
        public string Taluk { get; set; }
                
        public double Lat { get; set; }
                
        public double Lng { get; set; }

        [Required]
        public string CreatedDate { get; set; }

        [Required]
        public string UpdateDate { get; set; }

        [Required]
        public string Country { get; set; }
        public string Locality { get; set; }
        public string Status { get; set; }
    }
}
