using AutoMapper;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Core.Domain;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class SgstMasterProfile: Profile

    {
        public SgstMasterProfile()
        {
            // Default mapping when property names are same
            CreateMap<SgstMaster,SgstMasterModel>();
            CreateMap<SgstMasterModel,SgstMaster >();
        }

    }
}
