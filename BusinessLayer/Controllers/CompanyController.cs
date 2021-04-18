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
    /// This controller is used for CRUD and paging for Company information.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        public ICompanyService _companyService { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyService"></param>
        /// <param name="logger"></param>
        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        /// <summary>
        /// Get list of companies
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ServiceResponseModel<IEnumerable<CompanyModel>> response = new ServiceResponseModel<IEnumerable<CompanyModel>>();
            try
            {
                response = await _companyService.GetAll();
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
        /// Get all Company information by compCode,name
        /// </summary>
        /// <param name="pageParams"></param>
        /// <param name="getCompany"></param>
        /// <returns></returns>
        [Route("GetAllByPage")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, [FromBody] CompanyModel getCompany)
        {
            ServiceResponseModel<IEnumerable<CompInfo>> response = new ServiceResponseModel<IEnumerable<CompInfo>>();
            try
            {
                if (string.IsNullOrWhiteSpace(getCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                var compList = await _companyService.GetAll(pageParams, getCompany);
                Response.AddPaginationHeader(compList.CurrentPage, compList.PageSize, compList.TotalCount, compList.TotalPages);
                response.Data = compList;
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
        /// Get company by code. Required=>CompCode
        /// </summary>
        /// <param name="editCompany"></param>
        /// <returns></returns>
        [Route("GetCompanyByCode")]
        //[HttpGet()]
        [HttpPost]
        public async Task<IActionResult> GetCompanyByCode([FromBody] CompanyModel editCompany)
        {
            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                response = await _companyService.GetCompanyByCode(editCompany.CompCode);
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
        /// delte company. Required Company code 
        /// </summary>
        /// <param name="ByCode"></param>
        /// <returns></returns>
        [HttpDelete("{ByCode}")]
        public async Task<IActionResult> Delete(string ByCode)
        {
            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(ByCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                response = await _companyService.Delete(ByCode);
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
        /// Add new company. Required =>CompCode,AccYear,Name
        /// </summary>
        /// <param name="newCompany"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CompanyModel newCompany)
        {
            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }
                response = await _companyService.Add(newCompany);
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
        /// Edit Company. Required => CompCode+AccYear+Name 
        /// </summary>
        /// <param name="editCompany"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] CompanyModel editCompany)
        {

            ServiceResponseModel<CompanyModel> response = new ServiceResponseModel<CompanyModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(editCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(editCompany.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(editCompany.Name))
                {
                    throw new ArgumentNullException("Name is required");
                }
                response = await _companyService.Edit(editCompany);
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
        /// Create new year process. Required => CompCode+AccYear+NewYear
        /// </summary>
        /// <param name="newCompany"></param>
        /// <returns></returns>
        [HttpPost("NewYear")]
        public async Task<IActionResult> NewYear([FromBody] CompanyNewYearModel newCompany)
        {
            ServiceResponseModel<CompanyNewYearModel> response = new ServiceResponseModel<CompanyNewYearModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(newCompany.CompCode))
                {
                    throw new ArgumentNullException("CompCode is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.AccYear))
                {
                    throw new ArgumentNullException("AccYear is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.NewYear))
                {
                    throw new ArgumentNullException("NewYear is required");
                }
                if (string.IsNullOrWhiteSpace(newCompany.BalanceTransfer))
                {
                    throw new ArgumentNullException("BalanceTransfer is required");
                }

                response = await _companyService.NewYear(newCompany);
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
