using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphDBIntegration.Helper;
using GraphDBIntegration.Services;

namespace GraphDBIntegration.Commands
{
    public class ServiceBusMprPlansClient:IServiceBusMprPlansClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _serviceBusProcessor;
        private readonly IConfiguration _configuration;
        private readonly IStringHelper _stringHelper;
        private readonly ILogger _logger;
        private readonly IGraphClient _graphClient;

        public ServiceBusMprPlansClient(IStringHelper datetimeParse, IHttpClientFactory factory, IConfiguration configuration, ILogger<ServiceBusMprPlansClient> logger, IGraphClient graphClient)
        {
            _stringHelper = datetimeParse;
            _logger = logger;
            _configuration = configuration;
            _serviceBusClient = new ServiceBusClient(_configuration[_configuration[Constants.AppConfiguration.QueueConnection]]);
            ServiceBusProcessorOptions _serviceBusProcessorOptions = new()
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false,
            };
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(_configuration[Constants.AppConfiguration.MPRPlansQueue], _serviceBusProcessorOptions);
            _graphClient = graphClient;
        }
        public async Task Handle(IApplicationBuilder serviceProvider)
        {
            try
            {
                _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
                _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;

                await _serviceBusProcessor.StartProcessingAsync();
                _logger.LogWarning($"{nameof(ServiceBusMprPlansClient)} Service Started");
                
                

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
                if (args.Message != null)
                {
                    await _graphClient.GraphPush(args.Message);
                }
                await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await args.DeadLetterMessageAsync(args.Message).ConfigureAwait(false);
                _logger.LogError($"{ex.Message}");
            }
        }
    }
}
