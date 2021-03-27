using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class VoucherDetailProfile:Profile
    {
        public VoucherDetailProfile()
        {
            CreateMap<VoucherDetail, VoucherDetailModel>();
            CreateMap<VoucherDetailModel, VoucherDetail>();

        }
    }
}
