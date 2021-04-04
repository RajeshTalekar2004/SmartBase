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
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class IgstMasterController : ControllerBase
    {
        public readonly IIgstMasterService _igstService;
        public ILogger<IgstMasterController> _logger { get; }

        public IgstMasterController(IIgstMasterService igstService, ILogger<IgstMasterController> logger)
        {
            _igstService = igstService;
            _logger = logger;
        }

        /// <summary>
        /// Get all IGST code
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResponseModel<IEnumerable<IgstMasterModel>> response = new ServiceResponseModel<IEnumerable<IgstMasterModel>>();
            try
            {
                response = await _igstService.GetAll();
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
        /// Get all Igst Codes by IgstId,IgstDetail,IgstRate
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] IgstMasterModel getIgstMaster)
        {
            ServiceResponseModel<IEnumerable<IgstMaster>> response = new ServiceResponseModel<IEnumerable<IgstMaster>>();
            try
            {
                var igstList = await _igstService.GetAll(pageParams, getIgstMaster);
                Response.AddPaginationHeader(igstList.CurrentPage, igstList.PageSize, igstList.TotalCount, igstList.TotalPages);
                response.Data = igstList;
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
        /// Get IGST by Id
        /// </summary>
        /// <param name="igstId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{igstId}")]
        public async Task<IActionResult> GetIgstByCode([FromBody] IgstMasterModel editIgstMaster)
        {
            ServiceResponseModel<IgstMasterModel> response = new ServiceResponseModel<IgstMasterModel>();
            try
            {
                if (editIgstMaster.IgstId < 0)
                {
                    throw new ArgumentNullException("igstId is required");
                }
                response = await _igstService.GetIgstByCode(editIgstMaster.IgstId);
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
        /// Delete IGST Code
        /// </summary>
        /// <param name="igstId"></param>
        /// <returns></returns>
        [HttpDelete("{igstId}")]
        public async Task<IActionResult> Delete(int igstId)
        {
            ServiceResponseModel<IgstMasterModel> response = new ServiceResponseModel<IgstMasterModel>();
            try
            {
                if (igstId < 0)
                {
                    throw new ArgumentNullException("igstId is required");
                }
                response = await _igstService.Delete(igstId);
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
        /// Add new IGST code
        /// </summary>
        /// <param name="newIgstMaster"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] IgstMasterModel newIgstMaster)
        {
            ServiceResponseModel<IgstMasterModel> response = new ServiceResponseModel<IgstMasterModel>();
            try
            {
                if (newIgstMaster.IgstId < 0)
                {
                    throw new ArgumentNullException("IgstId is required");
                }
                if (string.IsNullOrWhiteSpace(newIgstMaster.IgstDetail))
                {
                    throw new ArgumentNullException("IgstDetail is required");
                }
                if (newIgstMaster.IgstId < 0)
                {
                    throw new ArgumentNullException("IgstId is required");
                }

                response = await _igstService.Add(newIgstMaster);
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
        /// Edit IGST code
        /// </summary>
        /// <param name="newIgstMaster"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] IgstMasterModel editIgstMaster)
        {
            ServiceResponseModel<IgstMasterModel> response = new ServiceResponseModel<IgstMasterModel>();
            try
            {
                if (editIgstMaster.IgstId < 0)
                {
                    throw new ArgumentNullException("IgstId is required");
                }
                if (string.IsNullOrWhiteSpace(editIgstMaster.IgstDetail))
                {
                    throw new ArgumentNullException("IgstDetail is required");
                }
                if (editIgstMaster.IgstId < 0)
                {
                    throw new ArgumentNullException("IgstId is required");
                }

                response = await _igstService.Edit(editIgstMaster);
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
