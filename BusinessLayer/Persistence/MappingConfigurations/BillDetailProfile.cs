using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class BillDetailProfile : Profile
    {
        public BillDetailProfile()
        {
            CreateMap<BillDetail, BillDetailModel>();
            CreateMap<BillDetailModel, BillDetail>();
        }
    }
}
