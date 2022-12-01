using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Services
{
    public interface ITableStorageCommands
    {
        Task<TableResult> TableStoragePush(string correlationId, string refId, string queueName, string Data);
    }
}
