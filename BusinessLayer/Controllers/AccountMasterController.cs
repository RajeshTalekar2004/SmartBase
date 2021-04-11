using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Manage Financial account master
    /// </summary>   
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountMasterController : ControllerBase
    {
        private IAccountMasterService _accountMasterService {get;}
        private readonly ILogger<AccountMasterController> _logger;

        /// <summary>
        /// Initialize account master controller
        /// </summary>
        /// <param name="accountMasterService"></param>
        /// <param name="logger"></param>
        public AccountMasterController(IAccountMasterService accountMasterService, ILogger<AccountMasterController> logger)
        {
            _accountMasterService = accountMasterService;
            _logger = logger;
        }

        /// <summary>
        /// Add new account Required CompCode+year+AccountId+Name
        /// </summary>
        /// <param name="newAccountMaster"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AccountMasterModel newAccountMaster)
        {
            ServiceResponseModel<AccountMasterModel> response = new ServiceResponseModel<AccountMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newAccountMaster.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newAccountMaster.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newAccountMaster.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }
                if (string.IsNullOrWhiteSpace(newAccountMaster.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }
                response = await _accountMasterService.Add(newAccountMaster);
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
        /// Delete account
        /// </summary>
        /// <param name="delAccountMaster"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public async Task<IActionResult> Delete([FromBody] AccountMasterModel delAccountMaster)
        {
            ServiceResponseModel<AccountMasterModel> response = new ServiceResponseModel<AccountMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(delAccountMaster.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(delAccountMaster.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(delAccountMaster.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }
                response = await _accountMasterService.Delete(delAccountMaster);
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
        /// Edit account. Required CompCode+year+AccountId+Name
        /// </summary>
        /// <param name="editAccountMaster"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] AccountMasterModel  editAccountMaster)
        {
           ServiceResponseModel<AccountMasterModel> response = new ServiceResponseModel<AccountMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editAccountMaster.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMaster.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMaster.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMaster.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }

                response = await _accountMasterService.Edit(editAccountMaster);
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
        /// Get account by code. Required CompCode+year+AccountId
        /// </summary>
        /// <param name="editAccountMasterModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByCode")]
        public async Task<IActionResult> GetAccountByCode([FromBody] AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> response = new ServiceResponseModel<AccountMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }

                response = await _accountMasterService.GetAccountByCode(editAccountMasterModel);
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
        /// Search AccountID start with wild card search. CompCode+year+AccountId
        /// </summary>
        /// <param name="editAccountMasterModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchByCode")]
        public async Task<IActionResult> SearchAccountByCode([FromBody] AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> response = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }

                response = await _accountMasterService.SearchAccountByCode(editAccountMasterModel);
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
        /// Get Account by name using company code and year. CompCode+year+Name
        /// </summary>
        /// <param name="editAccountMasterModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByName")]
        public async Task<IActionResult> GetAccountByName([FromBody] AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> response = new ServiceResponseModel<AccountMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }
                response = await _accountMasterService.GetAccountByName(editAccountMasterModel);
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
        /// Search Account Name start with. Wild card search. Required CompCode+year+Name
        /// </summary>
        /// <param name="editAccountMasterModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchByName")]
        public async Task<IActionResult> SearchAccountByName([FromBody] AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> response = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }

                response = await _accountMasterService.SearchAccountByName(editAccountMasterModel);
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
        /// Get all account by CompCode and year
        /// </summary>
        /// <param name="editAccountMasterModel"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody] AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> response = new  ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editAccountMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }

                response = await _accountMasterService.GetAll(editAccountMasterModel);
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
        /// Get all Account by accountId,name
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams , [FromBody] AccountMasterModel accountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMaster>> response = new ServiceResponseModel<IEnumerable<AccountMaster>>();

            try
            {
                if (string.IsNullOrWhiteSpace(accountMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(accountMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(accountMasterModel.OrderBy))
                {
                    accountMasterModel.OrderBy = "accountId";
                }

                var accountList = await _accountMasterService.GetAll(pageParams, accountMasterModel);
                Response.AddPaginationHeader(accountList.CurrentPage, accountList.PageSize, accountList.TotalCount, accountList.TotalPages);
                response.Data = accountList;
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
