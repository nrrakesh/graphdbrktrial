using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Helper
{
    public class AppConfigurationHelper
    {
        public string _strValue = string.Empty;
        public string GetAppConfigurationValue(string strkey)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            return root.GetSection(strkey).Value;
        }
        public string strValue
        {
            get => _strValue;
        }
        public void GetAppConfiguration(string strkey)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _strValue = root.GetSection(strkey).Value;
        }
    }
}
