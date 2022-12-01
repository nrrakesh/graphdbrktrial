using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Services
{
    public interface IServiceBusNudgeClient
    {
        Task Handle(IApplicationBuilder serviceProvider);
    }
}
