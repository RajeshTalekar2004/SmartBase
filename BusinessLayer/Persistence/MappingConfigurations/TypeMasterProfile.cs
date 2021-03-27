using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class TypeMasterProfile: Profile
    {
        public TypeMasterProfile()
        {
            CreateMap<TypeMaster, TypeMasterModel>();
            CreateMap<TypeMasterModel, TypeMaster>();
        }
    }
}
