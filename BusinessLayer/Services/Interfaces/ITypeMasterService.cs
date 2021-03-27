using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ITypeMasterService
    {
        Task<ServiceResponseModel<TypeMasterModel>> Add(TypeMasterModel newTypeMasterModel);

        Task<PagedList<TypeMaster>> GetAll(TypeParams typeParams);

        Task<ServiceResponseModel<TypeMasterModel>> Edit(TypeMasterModel editTypeMasterModel);
        Task<ServiceResponseModel<TypeMasterModel>> Delete(TypeMasterModel delTypeMasterModel);
        Task<ServiceResponseModel<IEnumerable<TypeMasterModel>>> GetAll(TypeMasterModel getTypeMasterModel);

        Task<ServiceResponseModel<TypeMasterModel>> GetTypeByCode(TypeMasterModel typeMasterModel);
    }
}
