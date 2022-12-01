using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.MPR
{
    public class MPRRechargePlansMasterData
    {
        public string RechargeId { get; set; }
        public string Operator { get; set; }
        public string Circle { get; set; }
        public double Amount { get; set; }
        public string RechargeTalktime { get; set; }
        public string RechargeValidity { get; set; }
        public string RechargeType { get; set; }
        public string VendorPlanUpdatedAt { get; set; }
        public long Version { get; set; }
        public string CreatedDateTime { get; set; }
        public string Category { get; set; }
        public string Data { get; set; }
        public string PlanId { get; set; }
        public string BatchId { get; set; }
        public long CategoryId { get; set; }
    }
}
