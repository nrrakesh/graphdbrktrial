using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphDBIntegration.Services
{
    public interface IServiceBusCustomerClient
    {
        Task Handle(IApplicationBuilder serviceProvider);
    }
}
