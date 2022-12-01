using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Helper
{
    public class Constants
    {
        public class GraphConfig
        {
            public const string schemaName = "Tvam_Trial_V1";
            public const string stream = "stream";
        }
        public class AppConfiguration
        {
            public const string QueueConnection = "QueueConnection";
            public const string CustomerQueue = "CustomerQueue";
            public const string InsuranceQueue = "InsuranceQueue";
            public const string PaymentQueue = "PaymentQueue";
            public const string TelemedicineQueue = "TelemedicineQueue";
            public const string PLQueue = "PLQueue";
            public const string BLQueue = "BLQueue";
            public const string UpiQueue = "UpiQueue";
            public const string MPRQueue = "MPRQueue";
            public const string NudgeQueue = "NudgeQueue";
            public const string MPRPlansQueue = "MPRPlansQueue";

            public const string GraphApi = "GraphApi";
            //Table Storage
            public const string StorageConn = "StorageConnectionString";
            public const string tableName = "GraphQueueData";
        }
        public class ConstantData
        {
            public const string Active = "Active";
            public const string Inactive = "Inactive";
        }
        public class UPIStatus
        {
            public const string Failure = "FAILURE";
            public const string Success = "SUCCESS";
            public const string Pending = "PENDING";
            public const string Timeout = "TIMEOUT";
        }
    }
}
