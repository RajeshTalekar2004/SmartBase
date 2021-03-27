using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VoucherMasterController : ControllerBase
    {
        private IVoucherMasterService _voucherMasterService;

        public VoucherMasterController(IVoucherMasterService voucherMasterService)
        {
            _voucherMasterService = voucherMasterService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newVoucherMasterModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add(VoucherMasterModel newVoucherMasterModel)
        {
            return Ok(await _voucherMasterService.Add(newVoucherMasterModel));
        }

        /// <summary>
        /// Delete Voucher Master CompCode+AccYear+VouNo is required field
        /// </summary>
        /// <param name="delVoucherMasterModel"></param>
        /// <returns></returns>
        [HttpDelete("{Delete}")]
        public  async Task<IActionResult> Delete(VoucherMasterModel delVoucherMasterModel)
        {
            return Ok(await _voucherMasterService.Delete(delVoucherMasterModel));
        }

        /// <summary>
        /// Edit Voucher Master CompCode+AccYear+VouNo is required field
        /// </summary>
        /// <param name="editVoucherMasterModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(VoucherMasterModel editVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> response = await _voucherMasterService.Edit(editVoucherMasterModel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Edit Voucher Master CompCode+AccYear+TrxType is required field
        /// </summary>
        /// <param name="editVoucherMasterModel"></param>
        /// <returns></returns>
        [Route("GetByTrxType")]
        [HttpGet]
        public async Task<IActionResult> GetByTrxType(VoucherMasterModel editVoucherMasterModel)
        {
            ServiceResponseModel<IEnumerable<VoucherMasterModel>> response = await _voucherMasterService.GetByTrxType(editVoucherMasterModel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Edit Voucher Master CompCode+AccYear+TrxType is required field
        /// </summary>
        /// <param name="editVoucherMasterModel"></param>
        /// <returns></returns>
        [Route("GetByVouNo")]
        [HttpGet]
        public async Task<IActionResult> GetByVouNo(VoucherMasterModel getVoucherMasterModel)
        {
            return Ok(await _voucherMasterService.GetByVouNo(getVoucherMasterModel));

        }


        [Route("GetAccountByTrxId")]
        [HttpGet]
        public async Task<IActionResult> GetAccountByTrxId([FromBody] VoucherMasterModel getVoucherMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> response = await _voucherMasterService.GetAccountByTrxId(getVoucherMasterModel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Get all Voucher Details by vouNo,accountId
        /// </summary>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] VoucherParams voucherParams)
        {
            if (string.IsNullOrWhiteSpace(voucherParams.CompCode))
            {
                throw new ArgumentNullException("CompCode is required");
            }
            if (string.IsNullOrWhiteSpace(voucherParams.AccYear))
            {
                throw new ArgumentNullException("AccYear is required");
            }
            if (string.IsNullOrWhiteSpace(voucherParams.TrxType))
            {
                throw new ArgumentNullException("TrxType is required");
            }
            var vouMstList = await _voucherMasterService.GetAll(voucherParams);
            Response.AddPaginationHeader(vouMstList.CurrentPage, vouMstList.PageSize, vouMstList.TotalCount, vouMstList.TotalPages);
            return Ok(vouMstList);
        }

    }
}
