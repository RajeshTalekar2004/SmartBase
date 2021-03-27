using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class TransactionMasterRepository : Repository<TransactionMaster>, ITransactionMasterRepository
    {
        public TransactionMasterRepository(SmartAccountContext context) : base(context)
        {
        }
        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
