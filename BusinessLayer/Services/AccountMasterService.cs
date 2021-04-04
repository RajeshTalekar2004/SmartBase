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
    public class AccountMasterService : IAccountMasterService
    {
        public AccountMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<AccountMasterModel>> Add(AccountMasterModel newAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> serviceResponse = new ServiceResponseModel<AccountMasterModel>();
            AccountMaster accounttMaster = _mapper.Map<AccountMaster>(newAccountMasterModel);
            await UnitOfWork.AccountMasters.AddAsync(accounttMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newAccountMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<AccountMasterModel>> Delete(AccountMasterModel delAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> serviceResponse = new ServiceResponseModel<AccountMasterModel>();
            AccountMaster delAccountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a => 
                                                a.CompCode == delAccountMasterModel.CompCode && 
                                                a.AccYear == delAccountMasterModel.AccYear && 
                                                a.AccountId == delAccountMasterModel.AccountId);
            UnitOfWork.AccountMasters.Remove(delAccountMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = delAccountMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<AccountMasterModel>> Edit(AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> serviceResponse = new ServiceResponseModel<AccountMasterModel>();
            AccountMaster editAccountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a => a.CompCode == editAccountMasterModel.CompCode &&  
                                                a.AccYear == editAccountMasterModel.AccYear &&   a.AccountId == editAccountMasterModel.AccountId);

            _mapper.Map<AccountMasterModel, AccountMaster>(editAccountMasterModel, editAccountMaster);
            serviceResponse.Data = editAccountMasterModel;
            UnitOfWork.AccountMasters.Update(editAccountMaster);
            await UnitOfWork.Complete();

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<AccountMasterModel>> GetAccountByCode(AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> serviceResponse = new ServiceResponseModel<AccountMasterModel>();
            AccountMaster dbAccountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a => 
                                                    a.CompCode == editAccountMasterModel.CompCode &&
                                                    a.AccYear == editAccountMasterModel.AccYear && 
                                                    a.AccountId == editAccountMasterModel.AccountId);
            AccountMasterModel accountMasterModel = _mapper.Map<AccountMasterModel>(dbAccountMaster);
            serviceResponse.Data = accountMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> SearchAccountByCode(AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            IEnumerable<AccountMaster> dbAccountMaster = await UnitOfWork.AccountMasters.FindAsync(a =>
                                                    a.CompCode == editAccountMasterModel.CompCode &&
                                                    a.AccYear == editAccountMasterModel.AccYear &&
                                                    a.AccountId.StartsWith(editAccountMasterModel.AccountId));



            IEnumerable < AccountMasterModel> accountMasterModel = _mapper.Map<IEnumerable<AccountMasterModel>>(dbAccountMaster);
            serviceResponse.Data = accountMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<AccountMasterModel>> GetAccountByName(AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<AccountMasterModel> serviceResponse = new ServiceResponseModel<AccountMasterModel>();
            AccountMaster dbAccountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a => 
                                                    a.CompCode == editAccountMasterModel.CompCode &&
                                                    a.AccYear == editAccountMasterModel.AccYear && 
                                                    a.Name == editAccountMasterModel.Name);
            AccountMasterModel accountMasterModel = _mapper.Map<AccountMasterModel>(dbAccountMaster);
            serviceResponse.Data = accountMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> SearchAccountByName(AccountMasterModel editAccountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            IEnumerable<AccountMaster> dbAccountMaster = await UnitOfWork.AccountMasters.FindAsync(a =>
                                                    a.CompCode == editAccountMasterModel.CompCode &&
                                                    a.AccYear == editAccountMasterModel.AccYear &&
                                                    a.Name.StartsWith(editAccountMasterModel.Name));
            IEnumerable<AccountMasterModel> accountMasterModel = _mapper.Map<IEnumerable<AccountMasterModel>>(dbAccountMaster);
            serviceResponse.Data = accountMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> GetAll(AccountMasterModel accountMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            IEnumerable<AccountMaster> dbAccountMasters = await UnitOfWork.AccountMasters.FindAsync(a => 
                                                    a.CompCode == accountMasterModel.CompCode &&
                                                    a.AccYear == accountMasterModel.AccYear );
            IEnumerable<AccountMasterModel> accountMasterModelAll = _mapper.Map<IEnumerable<AccountMasterModel>>(dbAccountMasters);
            serviceResponse.Data = accountMasterModelAll;

            return serviceResponse;
        }

        public async Task<PagedList<AccountMaster>> GetAll(PageParams pageParams, AccountMasterModel accountMasterModel)
        {
            var query = _context.AccountMasters
                        .Where(a=>a.CompCode== accountMasterModel.CompCode && a.AccYear == accountMasterModel.AccYear) 
                        .AsQueryable();

            switch (accountMasterModel.OrderBy)
            {
                case "accountId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.AccountId);
                    break;
                case "name":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.Name);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.AccountId);
                    break;
            }

            return await PagedList<AccountMaster>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }


    }
}
