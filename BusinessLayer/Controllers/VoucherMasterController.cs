using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Controllers
{
    /// <summary>
    /// This controller used for voucher entry CRUD / Paging
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VoucherMasterController : ControllerBase
    {
        private IVoucherMasterService _voucherMasterService;
        private readonly ILogger<TransactionMasterController> _logger;

        public VoucherMasterController(IVoucherMasterService voucherMasterService, ILogger<TransactionMasterController> logger)
        {
            _voucherMasterService = voucherMasterService;
            _logger = logger;
        }

        /// <summary>
        /// Add new voucher. Required CompCode+AccYear
        /// </summary>
        /// <param name="newVoucherMasterModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add(VoucherMasterModel newVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> response = new ServiceResponseModel<VoucherMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newVoucherMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newVoucherMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _voucherMasterService.Add(newVoucherMasterModel);
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
        /// Delete Voucher Master CompCode+AccYear+VouNo is required field
        /// </summary>
        /// <param name="delVoucherMasterModel"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public  async Task<IActionResult> Delete(VoucherMasterModel delVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> response = new ServiceResponseModel<VoucherMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(delVoucherMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(delVoucherMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _voucherMasterService.Delete(delVoucherMasterModel);
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
        /// Edit Voucher Master CompCode+AccYear+VouNo is required field
        /// </summary>
        /// <param name="editVoucherMasterModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(VoucherMasterModel editVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> response = new ServiceResponseModel<VoucherMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editVoucherMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editVoucherMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _voucherMasterService.Edit(editVoucherMasterModel);
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
        /// Get Voucher Master CompCode+AccYear+TrxType is required field
        /// </summary>
        /// <param name="editVoucherMasterModel"></param>
        /// <returns></returns>
        [Route("GetByTrxType")]
        [HttpPost]
        public async Task<IActionResult> GetByTrxType(VoucherMasterModel editVoucherMasterModel)
        {
            ServiceResponseModel<IEnumerable<VoucherMasterModel>> response = new ServiceResponseModel<IEnumerable<VoucherMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(editVoucherMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editVoucherMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _voucherMasterService.GetByTrxType(editVoucherMasterModel);
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
        ///  Get Voucher Master CompCode+AccYear is required field
        /// </summary>
        /// <param name="getVoucherMasterModel"></param>
        /// <returns></returns>
        [Route("GetByVouNo")]
        [HttpPost]
        public async Task<IActionResult> GetByVouNo(VoucherMasterModel getVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> response = new ServiceResponseModel<VoucherMasterModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(getVoucherMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                response = await _voucherMasterService.GetByVouNo(getVoucherMasterModel);
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
        /// Get account list by TrxCd. Required => CompCode+AccYear+TrxType
        /// </summary>
        /// <param name="getVoucherMasterModel"></param>
        /// <returns></returns>
        [Route("GetAccountByTrxId")]
        [HttpGet]
        public async Task<IActionResult> GetAccountByTrxId([FromBody] VoucherMasterModel getVoucherMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> response = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getVoucherMasterModel.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherMasterModel.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(getVoucherMasterModel.TrxType))
                {
                    throw new ArgumentNullException("Trx type is required");
                }
                response = await _voucherMasterService.GetAccountByTrxId(getVoucherMasterModel);
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
        /// Get all Voucher Details by vouNo,accountId. Required =>CompCode+AccYear+TrxType
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] VoucherMasterModel getVoucherMasterModel)
        {
            if (string.IsNullOrWhiteSpace(getVoucherMasterModel.CompCode))
            {
                throw new ArgumentNullException("CompCode is required");
            }
            if (string.IsNullOrWhiteSpace(getVoucherMasterModel.AccYear))
            {
                throw new ArgumentNullException("AccYear is required");
            }
            if (string.IsNullOrWhiteSpace(getVoucherMasterModel.TrxType))
            {
                throw new ArgumentNullException("TrxType is required");
            }
            var vouMstList = await _voucherMasterService.GetAll(pageParams, getVoucherMasterModel);
            Response.AddPaginationHeader(vouMstList.CurrentPage, vouMstList.PageSize, vouMstList.TotalCount, vouMstList.TotalPages);
            return Ok(vouMstList);
        }
    }
}
