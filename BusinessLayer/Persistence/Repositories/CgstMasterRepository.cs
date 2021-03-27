using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class CgstMasterRepository : Repository<CgstMaster>, ICgstMasterRepository
    {

        public CgstMasterRepository(SmartAccountContext context) : base(context)
        {

        }

        public Task<CgstMaster> GetCgstByCode(int SgstId)
        {
            return SmartAccountContext.CgstMasters.FirstOrDefaultAsync(s => s.CgstId == SgstId);
        }

        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
