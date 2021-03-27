using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class BillMasterProfile: Profile
    {
        public BillMasterProfile()
        {
            CreateMap<BillMaster, BillMasterModel>();
            CreateMap<BillMasterModel, BillMaster>();
        }
    }
}
