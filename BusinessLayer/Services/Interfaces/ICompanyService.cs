using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ServiceResponseModel<IEnumerable<CompanyModel>>> GetAll();

        Task<PagedList<CompInfo>> GetAll(CompInfoParams compInfoParams);

        Task<ServiceResponseModel<CompanyModel>> GetCompanyByCode(string compId);

        Task<ServiceResponseModel<CompanyModel>> Add(CompanyModel newCompany);

        Task<ServiceResponseModel<CompanyModel>> Edit(CompanyModel editCompany);

        Task<ServiceResponseModel<CompanyModel>> Delete(string compId);

        Task<ServiceResponseModel<CompanyNewYearModel>> NewYear(CompanyNewYearModel newCompany);
    }
}
