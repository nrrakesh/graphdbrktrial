using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphDBIntegration.Helper;
using GraphDBIntegration.Models.Query;
using GraphDBIntegration.Services;
using Microsoft.Extensions.Configuration;

namespace GraphDBIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryCommands _queryCommands;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public QueryController(ILogger<QueryController> logger, IQueryCommands queryCommands, IConfiguration config )
        {
            _queryCommands = queryCommands;
            _logger = logger;
            _config = config;
        }

        [HttpPost("db/databaseName/query")]
        public async Task<IActionResult> SqlQuery(string streamName)
        {
            ResponseData response;
            try
            {
                response = await _queryCommands.StreamQuery(Constants.GraphConfig.schemaName,streamName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} ");
                response = new ResponseData()
                {
                    Success = false,
                    Message = "Error",
                    Data = null
                };
            }
            return Ok(response);
        }
        [HttpGet("db/databaseName/list")]
        public async Task<IActionResult> DBList()
        {
            ResponseData response;
            try
            {
                response = await _queryCommands.DBList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} ");
                response = new ResponseData()
                {
                    Success = false,
                    Message = "Error",
                    Data = null
                };
            }
            return Ok(response);
        }
    }
}
                        
                    
                    

