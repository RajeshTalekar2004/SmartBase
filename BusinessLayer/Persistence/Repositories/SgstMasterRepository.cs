using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class SgstMasterRepository : Repository<SgstMaster>, ISgstMasterRepository
    {

        public SgstMasterRepository(SmartAccountContext context) : base(context)
        {

        }

        public Task<SgstMaster> GetSgstByCode(int SgstId)
        {
            return SmartAccountContext.SgstMasters.FirstOrDefaultAsync(s => s.SgstId == SgstId);
        }

        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }

    }
}
