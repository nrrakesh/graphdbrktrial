using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GraphDBIntegration.Models.Customer;
using GraphDBIntegration.Models.Query;
using GraphDBIntegration.Services;
using GraphDBIntegration.Helper;

namespace GraphDBIntegration.Commands
{
    public class QueryCommands:IQueryCommands
    {
        private HttpClient _apiClient;
        private readonly ILogger _logger;
        

        public QueryCommands(IHttpClientFactory factory, ILogger<QueryCommands> logger)
        {
            _apiClient = factory.CreateClient(Constants.AppConfiguration.GraphApi);
            _logger = logger;
        }
        public async Task<ResponseData> StreamQuery(string schemaName, string streamName)
        {
            ResponseData response;
            HttpResponseMessage responseMessage;
            
            SqlQuery sqlQuery = new()
            {
                sql = $"select * from {schemaName}.{streamName}"
            };
            try
            {
                responseMessage  = await _apiClient.PostAsync($"db/mydb/query", new StringContent(JsonConvert.SerializeObject(sqlQuery), System.Text.Encoding.UTF8, "application/json"));
                
                response = new ResponseData()
                {
                    Success = true,
                    Message = "Success",
                    Data = responseMessage.Content.ReadAsStringAsync().Result
                };
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex}\n{ex.Message} {schemaName} CustomerDetails");
                response = new ResponseData()
                {
                    Success = false,
                    Message = "Error",
                    Data = ex.ToString()
                };
            }
            return response;
            
        }
        public async Task<ResponseData> DBList()
        {
            ResponseData response;
            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await _apiClient.GetAsync($"db/mydb/list");

                response = new ResponseData()
                {
                    Success = true,
                    Message = "Success",
                    Data = responseMessage.Content.ReadAsStringAsync().Result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}\n{ex.Message}");
                response = new ResponseData()
                {
                    Success = false,
                    Message = "Error",
                    Data = ex.ToString()
                };
            }
            return response;

        }
    }
}
