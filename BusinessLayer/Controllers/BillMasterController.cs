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
    public class BillMasterController : ControllerBase 
    {
        private IBillMasterService _billMasterService {get;}
        private readonly ILogger<BillMasterController> _logger;
        public BillMasterController(IBillMasterService billMasterService, ILogger<BillMasterController> logger)
        {
            _billMasterService = billMasterService;
            _logger = logger;
        }

        /// <summary>
        /// Get Bill master list. CompCode + AccYear required field
        /// </summary>
        /// <param name="getBillMasterModel"></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody] BillMasterModel getBillMasterModel)
        {
            ServiceResponseModel<IEnumerable<BillMasterModel>> response = new ServiceResponseModel<IEnumerable<BillMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getBillMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getBillMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
               response = await _billMasterService.GetAll(getBillMasterModel);
                if (response.Data == null)
                {
                    return NotFound(response);
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all Bill master by paging. Sort by billId,accountId,billDate
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] BillMasterModel getBillMasterModel)
        {
            ServiceResponseModel<IEnumerable<BillMaster>> response = new ServiceResponseModel<IEnumerable<BillMaster>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getBillMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getBillMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                var billMasterList = await _billMasterService.GetAll(pageParams, getBillMasterModel);
                Response.AddPaginationHeader(billMasterList.CurrentPage, billMasterList.PageSize, billMasterList.TotalCount, billMasterList.TotalPages);
                response.Data = billMasterList;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Bill master record. CompCode + AccYear + AccountId + BillId required field
        /// </summary>
        /// <param name="billMasterId"></param>
        /// <returns></returns>
        [Route("GetBillId")]
        [HttpGet]
        public async Task<IActionResult>  GetBillId([FromBody] BillMasterModel billMasterId)
        {
            ServiceResponseModel<BillMasterModel> response = new ServiceResponseModel<BillMasterModel>();
            try
            {

                if (string.IsNullOrWhiteSpace(billMasterId.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterId.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterId.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterId.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }

                response = await _billMasterService.GetBillId(billMasterId);
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
        /// Add new record CompCode+AccYear+BillId+AccountID is required field
        /// </summary>
        /// <param name="newBillMasterModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] BillMasterModel newBillMasterModel)
        {
            ServiceResponseModel<BillMasterModel> response = new ServiceResponseModel<BillMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newBillMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newBillMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newBillMasterModel.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (string.IsNullOrWhiteSpace(newBillMasterModel.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }

                response = await _billMasterService.Add(newBillMasterModel);
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
        /// Edit record CompCode+AccYear+BillId+AccountID is required field
        /// </summary>
        /// <param name="editBillMasterModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] BillMasterModel editBillMasterModel)
        {

            ServiceResponseModel<BillMasterModel> response = new ServiceResponseModel<BillMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editBillMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editBillMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editBillMasterModel.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (string.IsNullOrWhiteSpace(editBillMasterModel.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }

                response = await _billMasterService.Edit(editBillMasterModel);
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
        /// Delete record CompCode+AccYear+BillId+AccountID is required field
        /// </summary>
        /// <param name="billMasterModel"></param>
        /// <returns></returns>
         [HttpDelete("{Delete}")]
        public async Task<IActionResult>  Delete([FromBody] BillMasterModel  billMasterModel)
        {
            ServiceResponseModel<BillMasterModel> response = new ServiceResponseModel<BillMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(billMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterModel.BillId))
                {
                    throw new ArgumentNullException("BillId is required");
                }
                if (string.IsNullOrWhiteSpace(billMasterModel.AccountId))
                {
                    throw new ArgumentNullException("AccountId is required");
                }
                response = await _billMasterService.Delete(billMasterModel);
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
