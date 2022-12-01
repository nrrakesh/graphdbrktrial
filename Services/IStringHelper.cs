using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Services
{
    public interface IStringHelper
    {
        string convertDate(string datetime);
        string convertCase(string status);
    }
}
