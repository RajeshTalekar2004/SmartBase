using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class CompanyRepository :Repository<CompInfo>, ICompanyRepository 
    {

        public CompanyRepository(SmartAccountContext context) : base(context)
        {
        }


        public Task<CompInfo> GetCompanyByCode(string compId)
        {
            return SmartAccountContext.CompInfos.FirstOrDefaultAsync(c => c.CompCode == compId);
        }


        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }

    }
}
