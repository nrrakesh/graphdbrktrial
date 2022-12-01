using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphDBIntegration.Helper;
using GraphDBIntegration.Services;
using Microsoft.Extensions.Logging;

namespace GraphDBIntegration.Commands
{
    public class TableStorageEntity : TableEntity
    {
        
        public TableStorageEntity(string correlationId)
        {
            this.PartitionKey = correlationId;
            this.RowKey = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
        }
        public TableStorageEntity() { } // the parameter-less constructor must be provided
        public string queueName { get; set; }
        public string RefId { get; set; }
        public string Data { get; set; }
    }
    public class TableStorageCommands: ITableStorageCommands
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public TableStorageCommands(ILogger<TableStorageCommands> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<TableResult> TableStoragePush(string correlationId, string refId, string queueName, string Data)
        {
            TableResult response;
            try
            {
                var storageAccount = CloudStorageAccount.Parse(_configuration[Constants.AppConfiguration.StorageConn]);
                var tableClient = storageAccount.CreateCloudTableClient();
                var table = tableClient.GetTableReference(Constants.AppConfiguration.tableName);
                await table.CreateIfNotExistsAsync();
                TableStorageEntity entity = new(correlationId)
                {
                    queueName = queueName,
                    Data = Data,
                    RefId = refId
                };
                response = await table.ExecuteAsync(TableOperation.Insert(entity));
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message} {ex} {correlationId} {refId}");
                return null;
            }
        }
    }
}
