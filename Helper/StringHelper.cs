using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphDBIntegration.Services;
using System.Globalization;

namespace GraphDBIntegration.Helper
{
    public class StringHelper:IStringHelper
    {
        public readonly ILogger _logger;
        public StringHelper(ILogger<StringHelper> logger)
        {
            _logger = logger;
        }
        public string convertDate(string datetime)
        {
            try
            {
                var dt = DateTime.Parse(datetime).ToString("yyyy-MM-dd HH:mm:ss");
                return dt;
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
            
        }
        public string convertCase(string status)
        {   
            try
            {
                var str = status.ToUpper();
                return str;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }
        }
    }
}
