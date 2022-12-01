using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Query
{
    public class ResponseData
    {
         
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    
    }
}
