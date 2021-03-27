using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class LedgerProfile : Profile
    {
        public LedgerProfile()
        {
            CreateMap<Ledger, LedgerModel>();
            CreateMap<LedgerModel, Ledger>();
        }
    }
}
