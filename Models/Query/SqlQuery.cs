using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Query
{
    public class SqlQuery
    {
        [JsonProperty]
        public string sql { get; set; }
    }
}
