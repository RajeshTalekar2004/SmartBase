using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {

        public UserRepository(SmartAccountContext context) : base(context)
        {

        }
        public Task<UserInfo> GetUserByName(string userName)
        {
            return SmartAccountContext.UserInfos.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<UserInfo> GetValidUser(string userName, string password)
        {
             return SmartAccountContext.UserInfos.SingleAsync(u => u.UserName == userName && u.UserPassword == password);
        }

        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
