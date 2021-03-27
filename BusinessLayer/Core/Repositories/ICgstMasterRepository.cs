using SmartBase.BusinessLayer.Core.Domain;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface ICgstMasterRepository: IRepository<CgstMaster>
    {
        public Task<CgstMaster>  GetCgstByCode(int SgstId);  

    }
}
