﻿using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ITransactionMasterService
    {
        Task<ServiceResponseModel<IEnumerable<TransactionMasterModel>>> GetAll();
        Task<PagedList<TransactionMaster>> GetAll(PageParams pageParams, TransactionMasterModel getTransactionMaster);
        Task<ServiceResponseModel<TransactionMasterModel>> GetByCode(string trxId);
        Task<ServiceResponseModel<TransactionMasterModel>> Add(TransactionMasterModel newTransactionMaster);
        Task<ServiceResponseModel<TransactionMasterModel>> Edit(TransactionMasterModel editTransactionMasterModel);
        Task<ServiceResponseModel<TransactionMasterModel>> Delete(string trxId);
    }
}
