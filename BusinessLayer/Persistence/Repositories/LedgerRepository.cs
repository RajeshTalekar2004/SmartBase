using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class LedgerRepository : Repository<Ledger>, ILedgerRepository
    {
        public LedgerRepository(SmartAccountContext context) : base(context)
        {
        }

        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
