using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IBillDetailService
    {
        Task<ServiceResponseModel<IEnumerable<BillDetailModel>>> GetAll(BillDetailModel getBillDetailModel);

        Task<PagedList<BillDetail>> GetAll(PageParams pageParams, BillDetailModel getBillDetailModel);

        Task<ServiceResponseModel<IEnumerable<BillDetailModel>>> GetBillId(BillDetailModel getBillDetailModel);

        Task<ServiceResponseModel<BillDetailModel>> Add(BillDetailModel newBillDetailModell);

        Task<ServiceResponseModel<BillDetailModel>> Edit(BillDetailModel editBillDetailModel);

        Task<ServiceResponseModel<BillDetailModel>> Delete(BillDetailModel billBillDetailModel);
    }
}
