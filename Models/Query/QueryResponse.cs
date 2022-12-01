using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphDBIntegration.Models.Customer;

namespace GraphDBIntegration.Models.Query
{
    public class QueryResponse
    {
        public List<ListRows> rows { get; set; }
        public int num_items { get; set; }
        public int more_data_to_come { get; set; }
        
    }
    public class ListRows
    {
        //public long k { get; set; }
        public string v { get; set; }
    }
}
