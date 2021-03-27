using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            // Default mapping when property names are same
            CreateMap<UserInfo, UserInfoModel>();
        }
    }
}
