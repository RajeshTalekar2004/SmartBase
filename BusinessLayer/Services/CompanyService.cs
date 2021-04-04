using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        public CompanyService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<IEnumerable<CompanyModel>>> GetAll()
        {
            ServiceResponseModel<IEnumerable<CompanyModel>> serviceResponse = new ServiceResponseModel<IEnumerable<CompanyModel>>();
            IEnumerable<CompInfo> dbCompanies = await UnitOfWork.Companies.GetAllAsync();
            IEnumerable<CompanyModel> userInfoModel = _mapper.Map<IEnumerable<CompanyModel>>(dbCompanies);
            serviceResponse.Data = userInfoModel.ToList();
            return serviceResponse;
        }


        public async Task<PagedList<CompInfo>> GetAll(PageParams pageParams, CompanyModel getCompany)
        {
            var query = _context.CompInfos
                        .Where(c => c.CompCode == getCompany.CompCode)
                        .AsQueryable();

            switch (getCompany.OrderBy)
            {
                case "compCode":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.YearBegin);
                    break;
                case "name":
                    query = query.OrderBy(c => c.Name).ThenBy(c=>c.YearBegin);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.YearBegin);
                    break;
            }

            return await PagedList<CompInfo>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }


        public async Task<ServiceResponseModel<CompanyModel>> GetCompanyByCode(string compId)
        {
            CompInfo dbCompany = await UnitOfWork.Companies.GetCompanyByCode(compId);
            CompanyModel companyModel = _mapper.Map<CompanyModel>(dbCompany);
            ServiceResponseModel<CompanyModel> serviceResponse = new ServiceResponseModel<CompanyModel> { Data = companyModel };
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<CompanyModel>> Add(CompanyModel newCompany)
        {
            ServiceResponseModel<CompanyModel> serviceResponse = new ServiceResponseModel<CompanyModel>();
            CompInfo company = _mapper.Map<CompInfo>(newCompany);
            await UnitOfWork.Companies.AddAsync(company);
            await UnitOfWork.Complete();
            serviceResponse.Data = newCompany;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<CompanyModel>> Edit(CompanyModel editCompanyModel)
        {
            ServiceResponseModel<CompanyModel> serviceResponse = new ServiceResponseModel<CompanyModel>();

            CompInfo editcomp = await UnitOfWork.Companies.SingleOrDefaultAsync(c => c.CompCode == editCompanyModel.CompCode);
            _mapper.Map<CompanyModel,CompInfo>(editCompanyModel, editcomp);
            UnitOfWork.Companies.Update(editcomp);
            await UnitOfWork.Complete();
            serviceResponse.Data = editCompanyModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<CompanyModel>> Delete(string compId)
        {
            ServiceResponseModel<CompanyModel> serviceResponse = new ServiceResponseModel<CompanyModel>();
            CompInfo delComp = await UnitOfWork.Companies.SingleOrDefaultAsync(c => c.CompCode == compId);
            UnitOfWork.Companies.Remove(delComp);
            await UnitOfWork.Complete();
            CompanyModel delCompModel = _mapper.Map<CompanyModel>(delComp);
            serviceResponse.Data = delCompModel;

            return serviceResponse;
        }

        public Task<ServiceResponseModel<CompanyNewYearModel>> NewYear(CompanyNewYearModel newCompany)
        {
            throw new System.NotImplementedException();
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }


    }
}
