using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Persistence;

namespace SmartBase.BusinessLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CgstMasterController : ControllerBase
    {
        private readonly ICgstMasterService _cgstMasterService;

        public ILogger<CgstMasterController> _logger { get; }

        /// <summary>
        /// Initilize CGST account controller
        /// </summary>
        /// <param name="cstMasterService"></param>
        public CgstMasterController(ICgstMasterService cstMasterService, ILogger<CgstMasterController> logger)
        {
            _cgstMasterService = cstMasterService;
            _logger = logger;
        }

        /// <summary>
        /// Get all CGST Codes
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResponseModel<IEnumerable<CgstMasterModel>> response = new  ServiceResponseModel<IEnumerable<CgstMasterModel>>();
            try
            {
                response = await _cgstMasterService.GetAll();
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
        /// Get all CGST order by cgstDetail,cgstRate,cgstId
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CgstParams cgstParams)
        {
            var cgstList = await _cgstMasterService.GetAll(cgstParams);
            Response.AddPaginationHeader(cgstList.CurrentPage, cgstList.PageSize, cgstList.TotalCount, cgstList.TotalPages);
            return Ok(cgstList);
        }


        /// <summary>
        /// Get CGST by code
        /// </summary>
        /// <param name="cgstId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{cgstId}")]
        public async Task<IActionResult> GetCgstByCode([FromQuery] CgstMasterModel cgstModel)
        {
            ServiceResponseModel<CgstMasterModel> response = new  ServiceResponseModel<CgstMasterModel>();
            try
            {
                if (cgstModel.CgstId < 0)
                {
                    throw new ArgumentNullException("CgstId is required");
                }
                response = await _cgstMasterService.GetCgstByCode(cgstModel.CgstId);
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
        /// Delete existing CGST code
        /// </summary>
        /// <param name="cgstId"></param>
        /// <returns></returns>
        [HttpDelete("{cgstId}")]
        public async Task<IActionResult> Delete(int cgstId)
        {
            ServiceResponseModel<CgstMasterModel> response = new  ServiceResponseModel<CgstMasterModel>();
            try
            {
                if (cgstId < 0)
                {
                    throw new ArgumentNullException("CgstId is required");
                }
                response = await _cgstMasterService.Delete(cgstId);
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
        /// <param name="newCgstMaster"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CgstMasterModel newCgstMaster)
        {
            ServiceResponseModel<CgstMasterModel> response = new  ServiceResponseModel<CgstMasterModel>();
            try
            {
                if (newCgstMaster.CgstId < 0)
                {
                    throw new ArgumentNullException("CgstId is required");
                }
                if (string.IsNullOrWhiteSpace(newCgstMaster.CgstDetail))
                {
                    throw new ArgumentNullException("CgstDetail is required");
                }
                if (newCgstMaster.CgstRate < 0)
                {
                    throw new ArgumentNullException("CgstRate is required");
                }

                response = await _cgstMasterService.Add(newCgstMaster);
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
        /// Edit existing CGST code
        /// </summary>
        /// <param name="editCgstMaster"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] CgstMasterModel editCgstMaster)
        {
            if (editCgstMaster.CgstId < 0)
            {
                throw new ArgumentNullException("CgstId is required");
            }
            if (string.IsNullOrWhiteSpace(editCgstMaster.CgstDetail))
            {
                throw new ArgumentNullException("CgstDetail is required");
            }
            if (editCgstMaster.CgstRate < 0)
            {
                throw new ArgumentNullException("CgstRate is required");
            }

            ServiceResponseModel<CgstMasterModel> response = new  ServiceResponseModel<CgstMasterModel>();
            try
            {
                response = await _cgstMasterService.Edit(editCgstMaster);
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
