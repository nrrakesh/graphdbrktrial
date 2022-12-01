using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Services
{
    public interface IGraphClient
    {
        Task GraphPush(ServiceBusReceivedMessage message);
    }
}
