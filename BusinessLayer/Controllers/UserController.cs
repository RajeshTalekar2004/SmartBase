using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartBase.BusinessLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<TransactionMasterController> _logger;

        public IUserService _userService { get; }

        /// <summary>
        /// Initiate user controller
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService,ILogger<TransactionMasterController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResponseModel<IEnumerable<UserInfoModel>> response = new ServiceResponseModel<IEnumerable<UserInfoModel>>();
            try
            {
                response = await _userService.GetAll();
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Get all user info by userName, userEmailId
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] UserInfoModel getUser)
        {
            ServiceResponseModel<IEnumerable<UserInfo>> response = new ServiceResponseModel<IEnumerable<UserInfo>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getUser.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                var usertList = await _userService.GetAll(pageParams, getUser);
                Response.AddPaginationHeader(usertList.CurrentPage, usertList.PageSize, usertList.TotalCount, usertList.TotalPages);
                response.Data = usertList;
            }
            catch(Exception ex)
            {

                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Get valid user by user name and password. Required UserName+UserPassword
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserInfoModel userinfo)
        {
            ServiceResponseModel<UserInfoModel>  response = new  ServiceResponseModel<UserInfoModel> ();
            try
            {
                if (string.IsNullOrWhiteSpace(userinfo.UserName))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                if (string.IsNullOrWhiteSpace(userinfo.UserPassword))
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                response = await _userService.Login(userinfo);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Refresh token using valid token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] UserInfoModel user)
        {
            ServiceResponseModel<UserInfoModel> response = new ServiceResponseModel<UserInfoModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                if (string.IsNullOrWhiteSpace(user.Token))
                {
                    throw new ArgumentNullException("Token is required");
                }
                response = await _userService.RefreshToken(user);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
             return Ok(response);
        }


        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [Route("ByName")]
        [HttpGet]
        public async Task<IActionResult> GetUserByName([FromBody] UserInfoModel userinfo)
        {
            ServiceResponseModel<UserInfoModel> response = new ServiceResponseModel<UserInfoModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(userinfo.UserName))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                response = await _userService.GetUserByName(userinfo.UserName);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Add new user. Required field CompCode+UserName+UserPassword
        /// </summary>
        /// <param name="newUsery"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserInfoModel newUsery)
        {
            ServiceResponseModel<UserInfoModel> response = new ServiceResponseModel<UserInfoModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newUsery.CompCode))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                if (string.IsNullOrWhiteSpace(newUsery.UserName))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                if (string.IsNullOrWhiteSpace(newUsery.UserPassword))
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                response = await _userService.Add(newUsery);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] UserInfoModel editUser)
        {
            ServiceResponseModel<UserInfoModel> response = new ServiceResponseModel<UserInfoModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editUser.CompCode))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                if (string.IsNullOrWhiteSpace(editUser.UserName))
                {
                    throw new ArgumentNullException("UserName is required");
                }
                if (string.IsNullOrWhiteSpace(editUser.UserPassword))
                {
                    throw new ArgumentNullException("UserPassword is required");
                }
                response = await _userService.Edit(editUser);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        ///  Delete user
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public async Task<IActionResult> Delete([FromBody] UserInfoModel userinfo)
        {
            ServiceResponseModel<UserInfoModel> response = new ServiceResponseModel<UserInfoModel>();
            try
            {
               if (string.IsNullOrWhiteSpace(userinfo.UserName))
                {
                    throw new ArgumentNullException("UserName is required");
                }
               response = await _userService.Delete(userinfo.UserName);
                if (response.Data == null)
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
