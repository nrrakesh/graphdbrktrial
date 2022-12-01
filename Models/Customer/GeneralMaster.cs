using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class GeneralMaster
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string MasID { get; set; }
        public string Value { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string MasDesc { get; set; }
    }
    public class GeneralMasterNew
    {
        public string ID { get; set; }
        public string MasType { get; set; }
        public string MasID { get; set; }
        public string Value { get; set; }
        public string IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string MasDesc { get; set; }
    }
}
