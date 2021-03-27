using SmartBase.BusinessLayer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface IAccountMasterRepository : IRepository<AccountMaster>
    {
        public  IQueryable<AccountMaster> GetAccountMasterByCode(string compId,string accyear, string accountId);
        public  IQueryable<AccountMaster> GetAccountMasterByName(string compId,string accyear, string name);
    }
}
