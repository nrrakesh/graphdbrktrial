using GraphDBIntegration.Commands;
using GraphDBIntegration.Helper;
using GraphDBIntegration.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GraphDBIntegration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            try
            {
                Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Information()
                                .WriteTo.AzureTableStorage(storageTableName: "GraphLogs", connectionString: Configuration[Constants.AppConfiguration.StorageConn])
                                .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
                                .CreateLogger();
                Log.Information("Starting up");
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
           
            byte[] rawdata = Properties.Resources.Cert;
            X509Certificate2 certificate = new X509Certificate2(rawdata, "tvam@1234", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet |
                    X509KeyStorageFlags.Exportable);
            services.AddHttpClient(Constants.AppConfiguration.GraphApi, options =>
            {
                options.BaseAddress = new Uri(Configuration["BangdbAPiBase"]);
                options.Timeout = TimeSpan.FromSeconds(30);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                },
                ClientCertificates = { new X509Certificate2(certificate) }
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphDBIntegration", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IStringHelper, StringHelper>();
            services.AddScoped<IQueryCommands, QueryCommands>();
            services.AddSingleton<ITableStorageCommands, TableStorageCommands>();
            services.AddSingleton<IGraphClient, GraphClient>();
            services.AddSingleton<IServiceBusBlClient, ServiceBusBlClient>();
            services.AddSingleton<IServiceBusCustomerClient, ServiceBusCustomerClient>();
            services.AddSingleton<IServiceBusInsuranceClient, ServiceBusInsuranceClient>();
            services.AddSingleton<IServiceBusMprClient, ServiceBusMprClient>();
            services.AddSingleton<IServiceBusNudgeClient, ServiceBusNudgeClient>();
            services.AddSingleton<IServiceBusPaymentClient, ServiceBusPaymentClient>();
            services.AddSingleton<IServiceBusPlClient, ServiceBusPlClient>();
            services.AddSingleton<IServiceBusTelemedicineClient, ServiceBusTelemedicineClient>();
            services.AddSingleton<IServiceBusUpiClient, ServiceBusUpiClient>();
            services.AddSingleton<IServiceBusMprPlansClient, ServiceBusMprPlansClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphDBIntegration v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var customerSubscription = app.ApplicationServices.GetService<IServiceBusCustomerClient>();
            var insuranceSubscription = app.ApplicationServices.GetService<IServiceBusInsuranceClient>();
            var mprSubscription = app.ApplicationServices.GetService<IServiceBusMprClient>();
            var nudgeSubscription = app.ApplicationServices.GetService<IServiceBusNudgeClient>();
            var paymentSubscription = app.ApplicationServices.GetService<IServiceBusPaymentClient>();
            var plSubscription = app.ApplicationServices.GetService<IServiceBusPlClient>();
            var blSubscription = app.ApplicationServices.GetService<IServiceBusBlClient>();
            var telemedicineSubscription = app.ApplicationServices.GetService<IServiceBusTelemedicineClient>();
            var upiSubscription = app.ApplicationServices.GetService<IServiceBusUpiClient>();
            var mprPlansSubscription = app.ApplicationServices.GetService<IServiceBusMprPlansClient>();

            customerSubscription.Handle(app).GetAwaiter().GetResult();
            insuranceSubscription.Handle(app).GetAwaiter().GetResult();
            mprSubscription.Handle(app).GetAwaiter().GetResult();
            nudgeSubscription.Handle(app).GetAwaiter().GetResult();
            paymentSubscription.Handle(app).GetAwaiter().GetResult();
            plSubscription.Handle(app).GetAwaiter().GetResult();
            blSubscription.Handle(app).GetAwaiter().GetResult();
            telemedicineSubscription.Handle(app).GetAwaiter().GetResult();
            upiSubscription.Handle(app).GetAwaiter().GetResult();
            mprPlansSubscription.Handle(app).GetAwaiter().GetResult();
        }
    }
}
