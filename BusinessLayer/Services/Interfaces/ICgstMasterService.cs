using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ICgstMasterService
    {
        Task<ServiceResponseModel<IEnumerable<CgstMasterModel>>> GetAll();

        Task<PagedList<CgstMaster>> GetAll(PageParams pageParams, CgstMasterModel getCgstMaster);

        Task<ServiceResponseModel<CgstMasterModel>> GetCgstByCode(int cgstId);

        Task<ServiceResponseModel<CgstMasterModel>> Add(CgstMasterModel newCgstMaster);

        Task<ServiceResponseModel<CgstMasterModel>> Edit(CgstMasterModel editCgstMaster);

        Task<ServiceResponseModel<CgstMasterModel>> Delete(int cgstId);
    }
}
