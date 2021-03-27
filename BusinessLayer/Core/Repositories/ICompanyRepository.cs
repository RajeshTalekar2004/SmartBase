using SmartBase.BusinessLayer.Core.Domain;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface ICompanyRepository : IRepository<CompInfo>
    {
        public  Task<CompInfo> GetCompanyByCode(string compId);
    }
}
