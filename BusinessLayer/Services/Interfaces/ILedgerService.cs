using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services.Interfaces
{
    public interface ILedgerService
    {
        Task<ServiceResponseModel<IEnumerable<LedgerModel>>> GetAll(LedgerModel ledgerModel);
        Task<PagedList<Ledger>> GetAll(PageParams pageParams, LedgerModel getledgerModel);
        Task<ServiceResponseModel<IEnumerable<LedgerModel>>> GetByCode(LedgerModel editLedgerModel);
        Task<ServiceResponseModel<LedgerModel>> Add(LedgerModel newLedgerModel);
        Task<ServiceResponseModel<LedgerModel>> Edit(LedgerModel editLedgerModel);
        Task<ServiceResponseModel<LedgerModel>> Delete(LedgerModel delLedgerModel);
        Task<ServiceResponseModel<String>> Receate(LedgerModel ledgerModel);
        Task<ServiceResponseModel<String>> AgeWiseAnalysis(LedgerModel ledgerModel);
        Task<ServiceResponseModel<String>> TradingAccount(LedgerModel ledgerModel);
        Task<ServiceResponseModel<String>> ProfileAndLoss(LedgerModel ledgerModel);
        Task<ServiceResponseModel<String>> DebtorList(LedgerModel ledgerModel);
        Task<ServiceResponseModel<String>> DebtorOrCreditorList(LedgerModel ledgerModel);
        Task<ServiceResponseModel<SalePurchaseReportModel>> PurchaseRegister(ReportModel reportModel);
        Task<ServiceResponseModel<SalePurchaseReportModel>> SaleRegister(ReportModel reportModel);
        Task<ServiceResponseModel<LedgerReportModel>> GeneralLedger(ReportModel reportModel);
    }
}
