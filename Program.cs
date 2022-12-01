using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GraphDBIntegration.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"{ex.Message} Application startup failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((content, config) =>
                {
                    try
                    {
                        AppConfigurationHelper appConfigurationHelper = new();
                        var root = config.Build();
                        string vault = $"{ appConfigurationHelper.GetAppConfigurationValue("SecretURL")}/";
                        string clientID = appConfigurationHelper.GetAppConfigurationValue("ClientID");
                        string clientSecret = appConfigurationHelper.GetAppConfigurationValue("ClientSecret");
                        string TenantID = appConfigurationHelper.GetAppConfigurationValue("TenantID");

                        var secretClient = new SecretClient(new Uri($"{vault}"),
                                                              new ClientSecretCredential(TenantID, clientID, clientSecret));
            
                        config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

                    }
                    catch (Exception ex)
                    {
                        Log.Error($"{ex.Message}");
                    }
                })  
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
