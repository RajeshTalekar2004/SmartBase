using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IIgstMasterService
    {
        Task<ServiceResponseModel<IEnumerable<IgstMasterModel>>> GetAll();

        Task<PagedList<IgstMaster>> GetAll(PageParams pageParams, IgstMasterModel getIgstMaster);

        Task<ServiceResponseModel<IgstMasterModel>> GetIgstByCode(int igstId);

        Task<ServiceResponseModel<IgstMasterModel>> Add(IgstMasterModel newIgstMaster);

        Task<ServiceResponseModel<IgstMasterModel>> Edit(IgstMasterModel editIgstMaster);

        Task<ServiceResponseModel<IgstMasterModel>> Delete(int igstId);
    }
}
