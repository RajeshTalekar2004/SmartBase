using AutoMapper;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompInfo, CompanyModel>();
            CreateMap<CompanyModel,CompInfo>();
        }
    }
}
