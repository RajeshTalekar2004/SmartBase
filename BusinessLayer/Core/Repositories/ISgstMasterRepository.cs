using SmartBase.BusinessLayer.Core.Domain;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface ISgstMasterRepository:IRepository<SgstMaster>
    {
        public Task<SgstMaster>  GetSgstByCode(int SgstId);
    }
}
 