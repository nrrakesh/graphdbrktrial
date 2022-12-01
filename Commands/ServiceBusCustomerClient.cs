using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GraphDBIntegration.Helper;
using GraphDBIntegration.Services;

namespace GraphDBIntegration.Commands
{
    public class ServiceBusCustomerClient:IServiceBusCustomerClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _serviceBusProcessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IGraphClient _graphClient;
       

        public ServiceBusCustomerClient(IHttpClientFactory factory, IConfiguration configuration, ILogger<ServiceBusCustomerClient> logger, IGraphClient graphClient)
        {
            _logger = logger;
            _configuration = configuration;
            var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
            _serviceBusClient = new ServiceBusClient(_configuration[_configuration[Constants.AppConfiguration.QueueConnection]], clientOptions);
            
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(_configuration[Constants.AppConfiguration.CustomerQueue], new ServiceBusProcessorOptions());
            _graphClient = graphClient;
        }
        public async Task Handle(IApplicationBuilder serviceProvider)
        {
            try
            {
                _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
                _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;

                await _serviceBusProcessor.StartProcessingAsync();
                _logger.LogWarning($"{nameof(ServiceBusCustomerClient)} Service Started");
                
                

            }
            catch (Exception ex)
            {
                _logger.LogError($"{_serviceBusProcessor.FullyQualifiedNamespace} {ex}, {ex.Message}");
            }
            
        }
        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception.Message, "Message handler encountered an exception");
            _logger.LogWarning($"- ErrorSource: {args.ErrorSource}");
            _logger.LogWarning($"- Entity Path: {args.EntityPath}");
            _logger.LogWarning($"- FullyQualifiedNamespace: {args.FullyQualifiedNamespace}");
            Console.WriteLine(args.Exception.ToString());
            
            return Task.CompletedTask;
        }
        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            try
            {
                if(args.Message!=null)
                {
                    await _graphClient.GraphPush(args.Message);
                }
                await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
                
            }
            catch(Exception ex)
            {
                
                _logger.LogError($"{ex.Message}");
            }
        }

    }
}
