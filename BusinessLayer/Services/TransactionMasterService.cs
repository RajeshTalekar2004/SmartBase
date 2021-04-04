using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence
{
    public class TransactionMasterService : ITransactionMasterService
    {

        public TransactionMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<TransactionMasterModel>> Add(TransactionMasterModel newTransactionMaster)
        {
            ServiceResponseModel<TransactionMasterModel> serviceResponse = new ServiceResponseModel<TransactionMasterModel>();
            TransactionMaster transactionMaster = _mapper.Map<TransactionMaster>(newTransactionMaster);
            await UnitOfWork.TransactionMasters.AddAsync(transactionMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newTransactionMaster;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<TransactionMasterModel>> Delete(string trxId)
        {
            ServiceResponseModel<TransactionMasterModel> serviceResponse = new ServiceResponseModel<TransactionMasterModel>();
            TransactionMaster delTransactionMaster = await UnitOfWork.TransactionMasters.SingleOrDefaultAsync(a =>
                                                        a.TrxId == trxId);
            UnitOfWork.TransactionMasters.Remove(delTransactionMaster);
            await UnitOfWork.Complete();
            TransactionMasterModel delTransactionMasterModel = _mapper.Map<TransactionMasterModel>(delTransactionMaster);
            serviceResponse.Data = delTransactionMasterModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<TransactionMasterModel>> Edit(TransactionMasterModel editTransactionMasterModel)
        {
            ServiceResponseModel<TransactionMasterModel> serviceResponse = new ServiceResponseModel<TransactionMasterModel>();
            TransactionMaster editTransactionMaster = await UnitOfWork.TransactionMasters.SingleOrDefaultAsync(a =>
                                                        a.TrxId == editTransactionMasterModel.TrxId);

            _mapper.Map<TransactionMasterModel, TransactionMaster>(editTransactionMasterModel, editTransactionMaster);
            serviceResponse.Data = editTransactionMasterModel;
            UnitOfWork.TransactionMasters.Update(editTransactionMaster);
            await UnitOfWork.Complete();

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<TransactionMasterModel>>> GetAll()
        {
            ServiceResponseModel<IEnumerable<TransactionMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<TransactionMasterModel>>();
            IEnumerable<TransactionMaster> dbTransactionMaster = await UnitOfWork.TransactionMasters.GetAllAsync();
            IEnumerable<TransactionMasterModel> TransactionMasterAll = _mapper.Map<IEnumerable<TransactionMasterModel>>(dbTransactionMaster);
            serviceResponse.Data = TransactionMasterAll;
            return serviceResponse;
        }

        public async Task<PagedList<TransactionMaster>> GetAll(PageParams pageParams, TransactionMasterModel getTransactionMaster)
        {
            var query = _context.TransactionMasters.AsQueryable();

            switch (getTransactionMaster.OrderBy)
            {
                case "trxId":
                    query = query.OrderBy(c => c.TrxId);
                    break;
                default:
                    query = query.OrderBy(c => c.TrxId);
                    break;
            }

            return await PagedList<TransactionMaster>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }


        public async Task<ServiceResponseModel<TransactionMasterModel>> GetByCode(string trxId)
        {
            ServiceResponseModel<TransactionMasterModel> serviceResponse = new ServiceResponseModel<TransactionMasterModel>();
            TransactionMaster editTransactionMaster = await UnitOfWork.TransactionMasters.SingleOrDefaultAsync(a =>
                                                         a.TrxId == trxId);
            TransactionMasterModel TransactionMasterAll = _mapper.Map<TransactionMasterModel>(editTransactionMaster);
            serviceResponse.Data = TransactionMasterAll;
            return serviceResponse;
        }
        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }

    }
}
