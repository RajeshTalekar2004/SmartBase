using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;

namespace SmartBase.BusinessLayer.Persistence.MappingConfigurations
{
    public class TransactionMasterProfile: Profile
    {
        public TransactionMasterProfile()
        {
            CreateMap<TransactionMaster, TransactionMasterModel>();
            CreateMap<TransactionMasterModel, TransactionMaster>();
        }
    }
}
