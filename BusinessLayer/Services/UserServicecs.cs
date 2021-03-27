using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Persistence.PageParams;
using System.Linq;

namespace SmartBase.BusinessLayer.Services
{
    public class UserServicecs : IUserService
    {
        public UserServicecs(SmartAccountContext context, IMapper mapper, ILogger<UserServicecs> logger,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserServicecs> _logger;

        public async Task<ServiceResponseModel<UserInfoModel>> Add(UserInfoModel newUser)
        {
            ServiceResponseModel<UserInfoModel> serviceResponse = new ServiceResponseModel<UserInfoModel>();
            PasswordService passwordService = new PasswordService();
            var salt = passwordService.SaltCreate();
            var hash = passwordService.HashCreate(newUser.UserPassword, salt);

            var userinfo =  new UserInfo
                                    {
                                        CompCode = newUser.CompCode,
                                        UserName = newUser.UserName,
                                        UserEmailId = newUser.UserEmailId,
                                        UserPassword = hash,
                                        UserSalt = salt
                                    };
            await UnitOfWork.Users.AddAsync(userinfo);
            await UnitOfWork.Complete();
            UserInfo dbUser = await UnitOfWork.Users.GetUserByName(newUser.UserName);
            UserInfoModel userInfoModel = _mapper.Map<UserInfoModel>(dbUser);

            serviceResponse.Data = userInfoModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<UserInfoModel>>> GetAll()
        {
            ServiceResponseModel<IEnumerable<UserInfoModel>> serviceResponse = new ServiceResponseModel<IEnumerable<UserInfoModel>>();
            IEnumerable<UserInfo> dbUsers = await UnitOfWork.Users.GetAllAsync();
            IEnumerable<UserInfoModel> userInfoModel = _mapper.Map<IEnumerable<UserInfoModel>>(dbUsers);
            serviceResponse.Data = userInfoModel;
            return serviceResponse;
        }

        public async Task<PagedList<UserInfo>> GetAll(UserParams userParams)
        {
            var query = _context.UserInfos
                         .Where(a=>a.CompCode==userParams.CompCode)
                         .AsQueryable();

            switch (userParams.OrderBy)
            {
                case "userName":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.UserName);
                    break;
                case "userEmailId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.UserEmailId);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.UserName);
                    break;
            }

            return await PagedList<UserInfo>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<ServiceResponseModel<UserInfoModel>> GetUserByName(string userName)
        {
            UserInfo dbUser = await UnitOfWork.Users.GetUserByName(userName);
            UserInfoModel userInfoModel = _mapper.Map<UserInfoModel>(dbUser);
            ServiceResponseModel<UserInfoModel> serviceResponse = new ServiceResponseModel<UserInfoModel> { Data = userInfoModel };
            return serviceResponse;
        }


        public async Task<ServiceResponseModel<UserInfoModel>> Edit(UserInfoModel editUser)
        {
            ServiceResponseModel<UserInfoModel> serviceResponse = new ServiceResponseModel<UserInfoModel>();
            UserInfo userIfo = await UnitOfWork.Users.SingleOrDefaultAsync(u => u.UserName == editUser.UserName);
            userIfo.UserEmailId = editUser.UserEmailId;
            UnitOfWork.Users.Update(userIfo);
            await UnitOfWork.Complete();
            serviceResponse.Data = editUser;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<UserInfoModel>> Delete(string userName)
        {
            ServiceResponseModel<UserInfoModel> serviceResponse = new ServiceResponseModel<UserInfoModel>();
            UserInfo delUser = await UnitOfWork.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            UnitOfWork.Users.Remove(delUser);
            await UnitOfWork.Complete();
            UserInfoModel userInfoModel = _mapper.Map<UserInfoModel>(delUser);
            serviceResponse.Data = userInfoModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<UserInfoModel>> Login(UserInfoModel userinfo)
        {
            ServiceResponseModel<UserInfoModel> serviceResponse = new ServiceResponseModel<UserInfoModel>();
            try
            {
                PasswordService passwordService = new PasswordService();
                UserInfo dbuserInfo = await UnitOfWork.Users.GetUserByName(userinfo.UserName);
                var match = passwordService.HashValidate(userinfo.UserPassword, dbuserInfo.UserSalt, dbuserInfo.UserPassword);
                if (match)
                {
                    UserInfoModel userInfoModel = _mapper.Map<UserInfoModel>(dbuserInfo);
                    userInfoModel.Token = CreateToken(dbuserInfo);
                    serviceResponse = new ServiceResponseModel<UserInfoModel> { Data = userInfoModel };
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = string.Format("{0}", "Invalid user or password");
                }
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private string CreateToken(UserInfo userinfo)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userinfo.CompCode),
                new Claim(ClaimTypes.Name, userinfo.UserName),
                new Claim(ClaimTypes.Email, userinfo.UserEmailId),
                new Claim(ClaimTypes.Role, "Admin") //TODO Fix this defaulted to admin role
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtToken").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["AppSettings:AccessExpireMinutes"])),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponseModel<UserInfoModel>> RefreshToken(UserInfoModel user)
        {
            ServiceResponseModel<UserInfoModel> response = new ServiceResponseModel<UserInfoModel>();
            var tokenValidationParameters = new TokenValidationParameters();
            SecurityToken securityToken;

            try
            {
                tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(_configuration.GetSection("AppSettings:JwtToken").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(user.Token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                var EyHrGpn = principal.Identity.Name;

                UserInfo userMaster = await UnitOfWork.Users.SingleOrDefaultAsync(u =>
                                    u.UserName.ToLower().Equals(user.UserName.ToLower()));
                UserInfoModel userInfoModel = _mapper.Map<UserInfoModel>(userMaster);
                userInfoModel.Token = CreateToken(userMaster);

                if (null == userMaster)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }
                else
                {
                    response.Data = userInfoModel ;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogWarning(string.Format("{0}:Exception:{1}", "Refresh token failed.", ex.StackTrace));
            }

            return response;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
