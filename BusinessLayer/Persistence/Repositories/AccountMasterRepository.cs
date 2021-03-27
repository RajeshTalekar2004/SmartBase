using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System.Linq;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class AccountMasterRepository : Repository<AccountMaster>, IAccountMasterRepository
    {
        public AccountMasterRepository(SmartAccountContext context) : base(context)
        {
        }
        public IQueryable<AccountMaster> GetAccountMasterByCode(string compId, string accyear, string accountId)
        {
            //return SmartAccountContext.AccountMasters.FirstOrDefaultAsync(a => 
            //    a.CompCode == compId && a.AccYear == accyear && a.AccountId == accountId );

            return SmartAccountContext.AccountMasters.Where(a =>
                    a.CompCode == compId &&
                    a.AccYear == accyear &&
                    a.AccountId.Contains(accountId)).AsQueryable();

        }

        public IQueryable<AccountMaster> GetAccountMasterByName(string compId, string accyear, string name)
        {
            //return SmartAccountContext.AccountMasters.FirstOrDefaultAsync(a =>
            //   a.CompCode == compId && a.AccYear == accyear && a.Name == name);

            return SmartAccountContext.AccountMasters.Where(a =>
                    a.CompCode == compId &&
                    a.AccYear == accyear &&
                    a.Name.Contains(name)).AsQueryable();
        }

        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }


    }
}
