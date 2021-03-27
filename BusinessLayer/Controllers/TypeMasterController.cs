using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeMasterController : ControllerBase
    {
        private ITypeMasterService _typeMasterService;
        public ILogger<TransactionMasterController> _logger { get; }

        /// <summary>
        /// Initilize type controller
        /// </summary>
        /// <param name="typeMasterService"></param>
        public TypeMasterController(ITypeMasterService typeMasterService, ILogger<TransactionMasterController> logger)
        {
            _typeMasterService = typeMasterService;
            _logger = logger;
        }

        /// <summary>
        /// Add new type record. CompCode+AccYear+TrxCd required field
        /// </summary>
        /// <param name="newTypeMasterModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add(TypeMasterModel newTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> response = new ServiceResponseModel<TypeMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newTypeMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newTypeMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newTypeMasterModel.TrxCd))
                {
                    throw new ArgumentNullException("TrxCd is required");
                }
                response = await _typeMasterService.Add(newTypeMasterModel);
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
        /// Delete Type record CompCode+AccYear+TrxCd required field
        /// </summary>
        /// <param name="delTypeMasterModel"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public async Task<IActionResult> Delete([FromBody] TypeMasterModel delTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> response = new ServiceResponseModel<TypeMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(delTypeMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(delTypeMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(delTypeMasterModel.TrxCd))
                {
                    throw new ArgumentNullException("TrxCd is required");
                }
                response = await _typeMasterService.Delete(delTypeMasterModel);
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
        /// Edit Type record CompCode+AccYear+TrxCd required field
        /// </summary>
        /// <param name="editTypeMasterModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult>  Edit(TypeMasterModel editTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> response = new ServiceResponseModel<TypeMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editTypeMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editTypeMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editTypeMasterModel.TrxCd))
                {
                    throw new ArgumentNullException("TrxCd is required");
                }
                response = await _typeMasterService.Edit(editTypeMasterModel);
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
        /// Get Type records CompCode+AccYear required field
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll(TypeMasterModel editTypeMasterModel)
        {
            ServiceResponseModel<IEnumerable<TypeMasterModel>> response = new ServiceResponseModel<IEnumerable<TypeMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(editTypeMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editTypeMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _typeMasterService.GetAll(editTypeMasterModel);
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
        /// Get all TypeMaster by trdCd,trxDetail
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TypeParams typeParams)
        {
            if (string.IsNullOrWhiteSpace(typeParams.CompCode))
            {
                throw new ArgumentNullException("CompCode is required");
            }
            if (string.IsNullOrWhiteSpace(typeParams.AccYear))
            {
                throw new ArgumentNullException("AccYear is required");
            }
            var typeList = await _typeMasterService.GetAll(typeParams);
            Response.AddPaginationHeader(typeList.CurrentPage, typeList.PageSize, typeList.TotalCount, typeList.TotalPages);
            return Ok(typeList);
        }




        [Route("GetTypeByCode")]
        [HttpGet]
        public  async Task<IActionResult>  GetTypeByCode(TypeMasterModel typeMasterModel)
        {
           ServiceResponseModel<TypeMasterModel> response = new ServiceResponseModel<TypeMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(typeMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(typeMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(typeMasterModel.TrxCd))
                {
                    throw new ArgumentNullException("TrxCd is required");
                }
                response = await _typeMasterService.GetTypeByCode(typeMasterModel);
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
