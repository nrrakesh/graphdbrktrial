using AutoMapper;
using GraphDBIntegration.Models.Customer;
using GraphDBIntegration.Models.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<GeneralMaster, GeneralMasterNew>();
            CreateMap<TvamBLGeneralMaster, TvamBLGeneralMasterNew>();
        }
    }
}
