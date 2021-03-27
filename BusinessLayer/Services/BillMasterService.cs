using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services
{
    public class BillMasterService : IBillMasterService
    {
        public BillMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<BillMasterModel>> Add(BillMasterModel newBillMasterModel)
        {
            ServiceResponseModel<BillMasterModel> serviceResponse = new ServiceResponseModel<BillMasterModel>();
            BillMaster billMaster = _mapper.Map<BillMaster>(newBillMasterModel);
            await UnitOfWork.BillMasters.AddAsync(billMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newBillMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<BillMasterModel>> Delete(BillMasterModel billMasterModel)
        {
            ServiceResponseModel<BillMasterModel> serviceResponse = new ServiceResponseModel<BillMasterModel>();
            BillMaster delBillMaster = await UnitOfWork.BillMasters.SingleOrDefaultAsync(a =>
                                                a.CompCode == billMasterModel.CompCode &&
                                                a.AccYear == billMasterModel.AccYear &&
                                                a.BillId == billMasterModel.BillId &&
                                                a.AccountId == billMasterModel.AccountId);
            UnitOfWork.BillMasters.Remove(delBillMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = billMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<BillMasterModel>> Edit(BillMasterModel editBillMasterModel)
        {
            ServiceResponseModel<BillMasterModel> serviceResponse = new ServiceResponseModel<BillMasterModel>();
            BillMaster editBillMaster = await UnitOfWork.BillMasters.SingleOrDefaultAsync(a =>
                                                a.CompCode == editBillMasterModel.CompCode &&
                                                a.AccYear == editBillMasterModel.AccYear &&
                                                a.BillId == editBillMasterModel.BillId &&
                                                a.AccountId == editBillMasterModel.AccountId);
            _mapper.Map<BillMasterModel, BillMaster>(editBillMasterModel, editBillMaster);
            UnitOfWork.BillMasters.Update(editBillMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = editBillMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<BillMasterModel>>> GetAll(BillMasterModel getBillMasterModel)
        {
            ServiceResponseModel<IEnumerable<BillMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<BillMasterModel>>();
            IEnumerable<BillMaster> getBillMaster = await UnitOfWork.BillMasters.FindAsync(a =>
                                                   a.CompCode == getBillMasterModel.CompCode &&
                                                   a.AccYear == getBillMasterModel.AccYear);

           IEnumerable <BillMasterModel> billMasterModel = _mapper.Map<IEnumerable<BillMasterModel>>(getBillMaster);
            serviceResponse.Data = billMasterModel;
            return serviceResponse;
        }

        public async Task<PagedList<BillMaster>> GetAll(BillParams billParams)
        {
            var query = _context.BillMasters
                        .Where(a=>a.CompCode==billParams.CompCode && a.AccYear == billParams.AccYear) 
                        .AsQueryable();

            switch (billParams.OrderBy)
            {
                case "billId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.BillId);
                    break;
                case "accountId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.BillId).ThenBy(c => c.AccountId);
                    break;
                case "billDate":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.BillId).ThenBy(c => c.AccountId).ThenBy(c=>c.BillDate);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.BillId);
                    break;
            }

            return await PagedList<BillMaster>.CreateAsync(query, billParams.PageNumber, billParams.PageSize);
        }


        public async Task<ServiceResponseModel<BillMasterModel>> GetBillId(BillMasterModel billMasterId)
        {
            ServiceResponseModel<BillMasterModel> serviceResponse = new ServiceResponseModel<BillMasterModel>();
            BillMaster editBillMaster = await _context.BillMasters
                                                    .Include( b => b.BillDetails)
                                                    .Include( b => b.VoucherMasters)
                                                    .Where(a =>
                                                     a.CompCode == billMasterId.CompCode &&
                                                     a.AccYear == billMasterId.AccYear &&
                                                     a.BillId == billMasterId.BillId &&
                                                     a.AccountId == billMasterId.AccountId)
                                                    .SingleAsync();

            BillMasterModel billMasterModel = _mapper.Map<BillMasterModel>(editBillMaster);
            serviceResponse.Data = billMasterModel;
            return serviceResponse;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
