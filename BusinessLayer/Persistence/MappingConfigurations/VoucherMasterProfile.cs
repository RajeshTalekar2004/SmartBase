using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class VoucherMasterProfile : Profile
    {
        public VoucherMasterProfile()
        {
            CreateMap<VoucherMaster, VoucherMasterModel>();
            CreateMap<VoucherMasterModel, VoucherMaster>();
        }
    }
}
