using AutoMapper;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Core.Domain;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class AccountMasterProfile: Profile
    {

        public AccountMasterProfile()
        {
            CreateMap<AccountMaster, AccountMasterModel>();
            CreateMap<AccountMasterModel, AccountMaster>();
        }

    }
}
