
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponseModel<IEnumerable<UserInfoModel>>> GetAll();
        Task<PagedList<UserInfo>> GetAll(UserParams userParams);
        Task<ServiceResponseModel<UserInfoModel>> GetUserByName(string userName);
        Task<ServiceResponseModel<UserInfoModel>> Add(UserInfoModel newUser);
        Task<ServiceResponseModel<UserInfoModel>> Edit(UserInfoModel editUser);
        Task<ServiceResponseModel<UserInfoModel>> Delete(string userName);
        Task<ServiceResponseModel<UserInfoModel>> Login(UserInfoModel userinfo);
        Task<ServiceResponseModel<UserInfoModel>> RefreshToken(UserInfoModel userinfo);
    }
}
