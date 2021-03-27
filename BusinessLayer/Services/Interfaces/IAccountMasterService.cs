using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IAccountMasterService
    {
        Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> GetAll(AccountMasterModel accountMasterModel);
        Task<PagedList<AccountMaster>> GetAll(AccountParams accountParams);
        Task<ServiceResponseModel<AccountMasterModel>> GetAccountByCode(AccountMasterModel editAccountMasterModel);
        Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> SearchAccountByCode(AccountMasterModel editAccountMasterModel);
        Task<ServiceResponseModel<AccountMasterModel>> GetAccountByName(AccountMasterModel newAccountMaster);
        Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> SearchAccountByName(AccountMasterModel editAccountMasterModel);
        Task<ServiceResponseModel<AccountMasterModel>> Add(AccountMasterModel newAccountMaster);
        Task<ServiceResponseModel<AccountMasterModel>> Edit(AccountMasterModel editAccountMaster);
        Task<ServiceResponseModel<AccountMasterModel>> Delete(AccountMasterModel delAccountMaster);
    }
}
