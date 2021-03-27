using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class IgstMasterRepository: Repository<IgstMaster>, IIgstMasterRepository
    {
        public IgstMasterRepository(SmartAccountContext context) : base(context)
        {

        }

        public Task<IgstMaster> GetIgstByCode(int IgstId)
        {
            return SmartAccountContext.IgstMasters.FirstOrDefaultAsync(s => s.IgstId == IgstId);
        }
        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
