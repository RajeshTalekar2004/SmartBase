using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IVoucherDetailService
    {
        Task<ServiceResponseModel<VoucherDetailModel>> Add(VoucherDetailModel newVoucherDetailModel);
        Task<ServiceResponseModel<VoucherDetailModel>> Edit(VoucherDetailModel editVoucherDetailModel);
        Task<ServiceResponseModel<VoucherDetailModel>> Delete(VoucherDetailModel delVoucherDetailModel);
        Task<ServiceResponseModel<IEnumerable<VoucherDetailModel>>> GetByVouNo(VoucherDetailModel getVoucherDetailModel);
        Task<ServiceResponseModel<VoucherDetailModel>> GetByVouNoItemSr(VoucherDetailModel getVoucherDetailModel);
        Task<ServiceResponseModel<IEnumerable<VoucherDetailModel>>> GetAll(VoucherDetailModel editVoucherDetailModel);
        Task<PagedList<VoucherDetail>> GetAll(PageParams pageParams, VoucherDetailModel getVoucherDetailModel);
    }
}
