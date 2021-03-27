using AutoMapper;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Core.Domain;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class IgstMasterProfile: Profile
    {
        public IgstMasterProfile()
        {
            CreateMap<IgstMaster, IgstMasterModel>();
            CreateMap<IgstMasterModel, IgstMaster>();
        }
    }
}
