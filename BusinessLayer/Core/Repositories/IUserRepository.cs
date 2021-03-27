using SmartBase.BusinessLayer.Core.Domain;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface IUserRepository:IRepository<UserInfo>
    {
        public  Task<UserInfo> GetUserByName(string userName);

        public  Task<UserInfo> GetValidUser(string userName,string password);

    }
}
