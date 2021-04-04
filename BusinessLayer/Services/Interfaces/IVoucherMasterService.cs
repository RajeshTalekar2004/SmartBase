using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IVoucherMasterService
    {
        Task<ServiceResponseModel<VoucherMasterModel>> Add(VoucherMasterModel newVoucherMasterModel);
        Task<ServiceResponseModel<VoucherMasterModel>> Edit(VoucherMasterModel editVoucherMasterModel);
        Task<ServiceResponseModel<VoucherMasterModel>> Delete(VoucherMasterModel delVoucherMasterModel);
        Task<ServiceResponseModel<VoucherMasterModel>> GetByVouNo(VoucherMasterModel getVoucherMasterModel);
        Task<ServiceResponseModel<IEnumerable<VoucherMasterModel>>> GetByTrxType(VoucherMasterModel editVoucherMasterModel);
        Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> GetAccountByTrxId(VoucherMasterModel editVoucherMasterModel);
        Task<PagedList<VoucherMaster>> GetAll(PageParams pageParams, VoucherMasterModel getVoucherMasterModel);

    }
}
