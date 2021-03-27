using AutoMapper;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Core.Domain;


namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class CgstMasterProfile: Profile
    {

        public CgstMasterProfile()
        {
            CreateMap<CgstMaster, CgstMasterModel>();
            CreateMap<CgstMasterModel, CgstMaster>();
        }

    }
}
