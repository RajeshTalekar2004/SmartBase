using SmartBase.BusinessLayer.Core.Domain;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface IIgstMasterRepository:IRepository<IgstMaster>
    {
        public Task<IgstMaster>  GetIgstByCode(int IgstId);
    }
}
