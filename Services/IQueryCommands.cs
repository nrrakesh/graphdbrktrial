using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphDBIntegration.Models.Query;

namespace GraphDBIntegration.Services
{
    public interface IQueryCommands
    {
        Task<ResponseData> StreamQuery(string schemaName, string streamName);
        Task<ResponseData> DBList();
    }
}
