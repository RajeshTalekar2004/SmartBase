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
    /// This controller is used for CRUD and paging for State GST.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SgstMasterController : ControllerBase
    {
        public readonly ISgstMasterService _sgstService;

        public ILogger<SgstMasterController> _logger { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sgstService"></param>
        /// <param name="logger"></param>
        public SgstMasterController(ISgstMasterService sgstService, ILogger<SgstMasterController> logger)
        {
            _sgstService = sgstService;
            _logger = logger;
        }

        /// <summary>
        /// Get all SGST code
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResponseModel<IEnumerable<SgstMasterModel>> response = new ServiceResponseModel<IEnumerable<SgstMasterModel>>();
            try
            {
                response = await _sgstService.GetAll();
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
        /// Get all SGST by sgistId,sgstDetail,sgstRate
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] SgstMasterModel getSgstMaster)
        {
            ServiceResponseModel<IEnumerable<SgstMaster>> response = new ServiceResponseModel<IEnumerable<SgstMaster>>();
            try
            {
                var sgstList = await _sgstService.GetAll(pageParams, getSgstMaster);
                Response.AddPaginationHeader(sgstList.CurrentPage, sgstList.PageSize, sgstList.TotalCount, sgstList.TotalPages);
                response.Data = sgstList;
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
        /// Get SGST by Id. Required =>SgstId
        /// </summary>
        /// <param name="editSgstMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSgstByCode")]
        public async Task<IActionResult> GetSgstByCode([FromBody] SgstMasterModel editSgstMaster)
        {
            ServiceResponseModel<SgstMasterModel> response = new ServiceResponseModel<SgstMasterModel>();
            try
            {
                if (editSgstMaster.SgstId < 0)
                {
                    throw new ArgumentNullException("sgstId is required");
                }
                response = await _sgstService.GetSgstByCode(editSgstMaster.SgstId);
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
        /// Delete SGST Code
        /// </summary>
        /// <param name="sgstId"></param>
        /// <returns></returns>
        [HttpDelete("{sgstId}")]
        public async Task<IActionResult> Delete(int sgstId)
        {
            ServiceResponseModel<SgstMasterModel> response = new ServiceResponseModel<SgstMasterModel>();
            try
            {
                if (sgstId < 0)
                {
                    throw new ArgumentNullException("sgstId is required");
                }
                response = await _sgstService.Delete(sgstId);
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
        /// Add new CGST code
        /// </summary>
        /// <param name="newSgstMaster"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] SgstMasterModel newSgstMaster)
        {
            ServiceResponseModel<SgstMasterModel> response = new ServiceResponseModel<SgstMasterModel>();
            try
            {
                if (newSgstMaster.SgstId < 0)
                {
                    throw new ArgumentNullException("SgstId is required");
                }
                if (string.IsNullOrWhiteSpace(newSgstMaster.SgstDetail))
                {
                    throw new ArgumentNullException("SgstDetail is required");
                }
                if (newSgstMaster.SgstId< 0)
                {
                    throw new ArgumentNullException("SgstId is required");
                }

                response = await _sgstService.Add(newSgstMaster);
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
        /// 
        /// </summary>
        /// <param name="editSgstMaster"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] SgstMasterModel editSgstMaster)
        {
            ServiceResponseModel<SgstMasterModel> response = new ServiceResponseModel<SgstMasterModel>();
            try
            {
                if (editSgstMaster.SgstId < 0)
                {
                    throw new ArgumentNullException("SgstId is required");
                }
                if (string.IsNullOrWhiteSpace(editSgstMaster.SgstDetail))
                {
                    throw new ArgumentNullException("SgstDetail is required");
                }
                if (editSgstMaster.SgstId < 0)
                {
                    throw new ArgumentNullException("SgstId is required");
                }

                response = await _sgstService.Edit(editSgstMaster);
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