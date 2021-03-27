using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ISgstMasterService
    {
        Task<ServiceResponseModel<IEnumerable<SgstMasterModel>>> GetAll();

        Task<PagedList<SgstMaster>> GetAll(SgstParams sgstParams);

        Task<ServiceResponseModel<SgstMasterModel>> GetSgstByCode(int sgstId);

        Task<ServiceResponseModel<SgstMasterModel>> Add(SgstMasterModel newSgstMaster);

        Task<ServiceResponseModel<SgstMasterModel>> Edit(SgstMasterModel editSgstMaster);

        Task<ServiceResponseModel<SgstMasterModel>> Delete(int sgstId);

    }
}
