using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ServiceResponseModel<IEnumerable<CompanyModel>>> GetAll();

        Task<PagedList<CompInfo>> GetAll(PageParams pageParams, CompanyModel getCompany);

        Task<ServiceResponseModel<CompanyModel>> GetCompanyByCode(string compId);

        Task<ServiceResponseModel<CompanyModel>> Add(CompanyModel newCompany);

        Task<ServiceResponseModel<CompanyModel>> Edit(CompanyModel editCompany);

        Task<ServiceResponseModel<CompanyModel>> Delete(string compId);

        Task<ServiceResponseModel<CompanyNewYearModel>> NewYear(CompanyNewYearModel newCompany);
    }
}
