using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.SqlClient;
using SmartBase.BusinessLayer.Persistence.PageParams;

namespace SmartBase.BusinessLayer.Services
{
    public class LedgerService : ILedgerService
    {
        public LedgerService(SmartAccountContext context, IMapper mapper, ILogger<LedgerService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public SmartAccountContext _context { get; }
        public ILogger<LedgerService> _logger { get; }

        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<LedgerModel>> Add(LedgerModel newLedgerModel)
        {
            ServiceResponseModel<LedgerModel> serviceResponse = new ServiceResponseModel<LedgerModel>();
            Ledger ledger = _mapper.Map<Ledger>(newLedgerModel);
            await UnitOfWork.Ledgers.AddAsync(ledger);
            await UnitOfWork.Complete();
            serviceResponse.Data = newLedgerModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<LedgerModel>> Delete(LedgerModel delLedgerModel)
        {
            ServiceResponseModel<LedgerModel> serviceResponse = new ServiceResponseModel<LedgerModel>();
            Ledger delLedger = await UnitOfWork.Ledgers.SingleOrDefaultAsync(a =>
                                                a.CompCode == delLedgerModel.CompCode &&
                                                a.AccYear == delLedgerModel.AccYear &&
                                                a.VouNo == delLedgerModel.VouNo &&
                                                a.ItemSr == delLedgerModel.ItemSr);
            UnitOfWork.Ledgers.Remove(delLedger);
            await UnitOfWork.Complete();
            serviceResponse.Data = delLedgerModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<LedgerModel>> Edit(LedgerModel editLedgerModel)
        {
            ServiceResponseModel<LedgerModel> serviceResponse = new ServiceResponseModel<LedgerModel>();
            Ledger editLedger = await UnitOfWork.Ledgers.SingleOrDefaultAsync(a => 
                                                a.CompCode == editLedgerModel.CompCode &&
                                                a.AccYear == editLedgerModel.AccYear && 
                                                a.VouNo == editLedgerModel.VouNo &&
                                                a.ItemSr == editLedgerModel.ItemSr);
            _mapper.Map<LedgerModel, Ledger>(editLedgerModel, editLedger);
            serviceResponse.Data = editLedgerModel;
            UnitOfWork.Ledgers.Update(editLedger);
            await UnitOfWork.Complete();
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<LedgerModel>>> GetAll(LedgerModel ledgerModel)
        {
            ServiceResponseModel<IEnumerable<LedgerModel>> serviceResponse = new ServiceResponseModel<IEnumerable<LedgerModel>>();
            IEnumerable<Ledger> editLedger = await UnitOfWork.Ledgers.FindAsync(a =>
                                                    a.CompCode == ledgerModel.CompCode &&
                                                    a.AccYear == ledgerModel.AccYear);
            IEnumerable<LedgerModel> ledgerModelModelAll = _mapper.Map<IEnumerable<LedgerModel>>(editLedger);
            serviceResponse.Data = ledgerModelModelAll;

            return serviceResponse;
        }

        public async Task<PagedList<Ledger>> GetAll(LedgerParams ledgerParams)
        {
            var query = _context.Ledgers
                        .Where(a=>a.CompCode==ledgerParams.CompCode && a.AccYear == ledgerParams.AccYear) 
                        .AsQueryable();

            switch (ledgerParams.OrderBy)
            {
                case "vouNo":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.VouNo);
                    break;
                case "vouDate":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.VouDate).ThenBy(c=>c.VouNo);
                    break;
                case "accountId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.AccountId).ThenBy(c => c.VouDate);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.AccountId).ThenBy(c => c.VouDate);
                    break;
            }

            return await PagedList<Ledger>.CreateAsync(query, ledgerParams.PageNumber, ledgerParams.PageSize);
        }


        public async Task<ServiceResponseModel<IEnumerable<LedgerModel>>> GetByCode(LedgerModel editLedgerModel)
        {
            ServiceResponseModel<IEnumerable<LedgerModel>> serviceResponse = new ServiceResponseModel<IEnumerable<LedgerModel>>();
            IEnumerable<Ledger> editLedger = await UnitOfWork.Ledgers.FindAsync(a =>
                                                    a.CompCode == editLedgerModel.CompCode &&
                                                    a.AccYear == editLedgerModel.AccYear &&
                                                    a.AccountId == editLedgerModel.AccountId);
            IEnumerable<LedgerModel> ledgerModelModelAll = _mapper.Map<IEnumerable<LedgerModel>>(editLedger);
            serviceResponse.Data = ledgerModelModelAll;

            return serviceResponse;
        }


        public async Task<ServiceResponseModel<string>> Receate(LedgerModel editLedgerModel)
        {
            ServiceResponseModel<string> serviceResponse = new ServiceResponseModel<string>();

            int itemSr = 0;
            decimal mtot = 0;
            List<Ledger> ledgersRec = new List<Ledger>();
            Ledger ledger = null;
            string sqlQuery = null;
            List<VoucherMaster> voucherMasters ;

             _logger.LogInformation(String.Format("Ledger Recreation started for CompCode:{0} AccYear{1}",
                                        editLedgerModel.CompCode, editLedgerModel.AccYear));
            try
            {
                _logger.LogInformation("Account Master balance resetting");
                sqlQuery = String.Format("Update AccountMaster SET CurDr = 0 ,CurCr = 0, Closing = 0 WHERE CompCode='{0}' AND AccYear='{1}'",
                                editLedgerModel.CompCode, editLedgerModel.AccYear);
                _context.Database.ExecuteSqlRaw(sqlQuery);
                _logger.LogInformation("Account Master SET CurDr=0, CurCr=0 resetting");
                sqlQuery = String.Format("Update AccountMaster SET Closing = Opening WHERE CompCode='{0}' AND AccYear='{1}'",
                                                editLedgerModel.CompCode, editLedgerModel.AccYear);

                _context.Database.ExecuteSqlRaw(sqlQuery);

                var openingClosing = _context.AccountMasters
                                               .Where(a =>
                                                   a.CompCode == editLedgerModel.CompCode &&
                                                   a.AccYear == editLedgerModel.AccYear)
                                               .GroupBy(x => true)
                                               .Select(a => new { 
                                                            sumOpening = a.Sum(y => y.Opening),
                                                            sumClosing = a.Sum(y => y.Closing)
                                                        })
                                               .FirstOrDefault();
  
                 _logger.LogInformation(string.Format("Before Opening:{0} Closing:{1}",openingClosing.sumOpening,openingClosing.sumClosing));

                _logger.LogInformation("Removeing ledger records in progress");
                IEnumerable<Ledger> ledgerRecords = await UnitOfWork.Ledgers.FindAsync(t =>
                                            t.CompCode == editLedgerModel.CompCode &&
                                            t.AccYear == editLedgerModel.AccYear);

                UnitOfWork.Ledgers.RemoveRange(ledgerRecords);
                await UnitOfWork.Complete();
               _logger.LogInformation("Removed ledger records");

                voucherMasters = await _context.VoucherMasters
                                        .Include(vm => vm.VoucherDetails)
                                        .Include(vm => vm.Ledgers)
                                        .Include(vm => vm.Sgst)
                                        .Include(vm => vm.Cgst)
                                        .Where(t =>
                                            t.CompCode == editLedgerModel.CompCode &&
                                            t.AccYear == editLedgerModel.AccYear)
                                        .OrderBy(vm => vm.VouNo)
                                        .ToListAsync();


                foreach (VoucherMaster voucherMaster in voucherMasters)
                {
                    _logger.LogInformation(String.Format("Ledger Recreation for VouNo:{0} Date{1} started.", voucherMaster.VouNo,voucherMaster.VouDate));

                    mtot = 0;
                    itemSr = 1;
                    mtot = voucherMaster.DrCr == "1" ? mtot - (decimal)voucherMaster.VouAmount : mtot + (decimal)voucherMaster.VouAmount;
                    VoucherDetail vouDetailmaster = voucherMaster.VoucherDetails.First();
                    //First ledger record
                    ledger = new Ledger
                    {
                        CompCode = voucherMaster.CompCode,
                        AccYear = voucherMaster.AccYear,
                        VouNo = voucherMaster.VouNo,
                        VouDate = voucherMaster.VouDate,
                        TrxType = voucherMaster.TrxType,
                        BilChq = voucherMaster.BilChq,
                        ItemSr = itemSr,
                        AccountId = voucherMaster.AccountId,
                        DrCr = voucherMaster.DrCr,
                        Amount = voucherMaster.VouAmount,
                        CorrAccountId = vouDetailmaster.AccountId,
                        VouDetail = vouDetailmaster.VouDetail,
                        VoucherMaster = voucherMaster
                    };
                    ledgersRec.Add(ledger);
                   

                    foreach (VoucherDetail voucherDetail in voucherMaster.VoucherDetails)
                    {
                        itemSr = itemSr + 1;
                        mtot = voucherDetail.DrCr == "1" ? mtot - (decimal) voucherDetail.Amount : mtot + (decimal) voucherDetail.Amount;

                        ledger = new Ledger
                        {
                            CompCode = voucherMaster.CompCode,
                            AccYear = voucherMaster.AccYear,
                            VouNo = voucherMaster.VouNo,
                            VouDate = voucherMaster.VouDate,
                            TrxType = voucherMaster.TrxType,
                            BilChq = voucherMaster.BilChq,
                            ItemSr = itemSr,
                            AccountId = voucherDetail.AccountId,
                            DrCr = voucherDetail.DrCr,
                            Amount = voucherDetail.Amount,
                            CorrAccountId = voucherMaster.AccountId,
                            VouDetail = voucherDetail.VouDetail,
                            VoucherMaster = voucherMaster
                        };
                        ledgersRec.Add(ledger);
                    }

                    await UnitOfWork.Ledgers.AddRangeAsync(ledgersRec);

                    if (mtot != 0)
                    {
                        _logger.LogWarning(string.Format("{0}{1}", "Debit/Credit total mismatch for voucher :", voucherMaster.VouNo));
                    }

                    foreach (Ledger ledgerTmp in ledgersRec)
                    {
                        AccountMaster accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                                           a.CompCode == ledgerTmp.CompCode &&
                                                                           a.AccYear == ledgerTmp.AccYear &&
                                                                           a.AccountId == ledgerTmp.AccountId);
                        if (ledgerTmp.DrCr == "1")
                        {
                            accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault() + ledgerTmp.Amount;
                            accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                        }
                        else
                        {
                            accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() + ledgerTmp.Amount;
                            accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                        }
                        UnitOfWork.AccountMasters.Update(accountMaster);
                    }
                    _logger.LogInformation(String.Format("Ledger Recreation for VouNo:{0} Date{1} Completed.", voucherMaster.VouNo, voucherMaster.VouDate));
                }

                await UnitOfWork.Complete();


                openingClosing = _context.AccountMasters
                                               .Where(a =>
                                                   a.CompCode == editLedgerModel.CompCode &&
                                                   a.AccYear == editLedgerModel.AccYear)
                                               .GroupBy(x => true)
                                               .Select(a => new
                                               {
                                                   sumOpening = a.Sum(y => y.Opening),
                                                   sumClosing = a.Sum(y => y.Closing)
                                               })
                                               .FirstOrDefault();
                _logger.LogInformation(string.Format("After Opening:{0} Closing:{1}", openingClosing.sumOpening, openingClosing.sumClosing));
                serviceResponse.Data = string.Format("After Opening:{0} Closing:{1}", openingClosing.sumOpening, openingClosing.sumClosing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public Task<ServiceResponseModel<string>> AgeWiseAnalysis(LedgerModel ledgerModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseModel<string>> TradingAccount(LedgerModel ledgerModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseModel<string>> ProfileAndLoss(LedgerModel ledgerModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseModel<string>> DebtorList(LedgerModel ledgerModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseModel<string>> DebtorOrCreditorList(LedgerModel ledgerModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseModel<SalePurchaseReportModel>> PurchaseRegister(ReportModel reportModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseModel<SalePurchaseReportModel>> SaleRegister(ReportModel reportModel)
        {
            ServiceResponseModel<SalePurchaseReportModel> serviceResponse = new ServiceResponseModel<SalePurchaseReportModel>();

            SalePurchaseReportModel saleOrPurchaseReport = new SalePurchaseReportModel();
            saleOrPurchaseReport.SalePurchaseDetails = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.SalePurchaseDetailsTotal = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.AccountHeadSummary = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.AccountHeadSummaryTotal = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.CgstHeadSummary = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.CgstHeadSummaryTotal = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.SgstHeadSummary = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.SgstHeadSummaryTotal = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.IgstHeadSummary = new List<SalePurchaseDetailModel>();
            saleOrPurchaseReport.IgstHeadSummaryTotal = new List<SalePurchaseDetailModel>();

            Decimal vouAmount = 0;
            Decimal netAmount = 0;
            Decimal sgstAmount = 0;
            Decimal cgstAmount = 0;
            Decimal igstAmount = 0;

            saleOrPurchaseReport.Company = _context.CompInfos.FirstOrDefault(a =>
                                     a.CompCode == reportModel.CompCode &&
                                     a.AccYear == reportModel.AccYear);


            //Select sales or purchase records
            IEnumerable<SalePurchaseDetailModel> registerDetail = (from vouMst in _context.VoucherMasters
                                                                  join acMst in _context.AccountMasters on vouMst.AccountId equals acMst.AccountId
                                                                  join cgstMst in _context.CgstMasters on vouMst.CgstId equals cgstMst.CgstId into cgstTemp  
                                                                        from cgstTemp00 in cgstTemp.DefaultIfEmpty()
                                                                  join sgstMst in _context.SgstMasters on vouMst.SgstId equals sgstMst.SgstId into sgstTemp  
                                                                        from sgstTemp00 in sgstTemp.DefaultIfEmpty()
                                                                  join igstMst in _context.IgstMasters on vouMst.IgstId equals igstMst.IgstId into igstTemp
                                                                        from igstTemp00 in igstTemp.DefaultIfEmpty()
                                                                  where (vouMst.CompCode==reportModel.CompCode && vouMst.AccYear==reportModel.AccYear && vouMst.TrxType==reportModel.SaleOrPurchaseType
                                                                            && vouMst.VouDate >= reportModel.StartDate && vouMst.VouDate <= reportModel.FinishDate) 
                                                                  select new SalePurchaseDetailModel
                                                                  {
                                                                      CompCode = vouMst.CompCode,
                                                                      AccYear = vouMst.AccYear,
                                                                      VouNo = vouMst.VouNo,
                                                                      VouDate = vouMst.VouDate,
                                                                      AccountId = vouMst.AccountId,
                                                                      AccountName = acMst.Name,
                                                                      TrxType = vouMst.TrxType,
                                                                      BilChq = vouMst.BilChq, 
                                                                      DrCr = vouMst.DrCr,
                                                                      VouAmount = vouMst.VouAmount,
                                                                      NetAmount = vouMst.NetAmount,
                                                                      SgstAmount = vouMst.SgstAmount,
                                                                      CgstAmount = vouMst.CgstAmount,
                                                                      IgstAmount = vouMst.IgstAmount,
                                                                      CgstId = vouMst.CgstId,
                                                                      CgstName = cgstTemp00.CgstDetail,
                                                                      SgstId = vouMst.SgstId,
                                                                      SgstName = sgstTemp00.SgstDetail,
                                                                      IgstId = vouMst.IgstId,
                                                                      IgstName = igstTemp00.IgstDetail
                                                                  })
                                                                 .OrderBy(a => a.CompCode).ThenBy(a => a.AccYear).ThenBy(a => a.VouDate)
                                                                 .ToList();
            //Add detail rows
            saleOrPurchaseReport.SalePurchaseDetails.AddRange(registerDetail);
            //Add grand total of detail rows
            var salePurchaseTotals = registerDetail
                                      .GroupBy(a => 1)
                                      .Select(a => new
                                      {
                                          AccountName = a.Key,
                                          VouAmount = a.Sum(y => y.VouAmount),
                                          NetAmount = a.Sum(y => y.NetAmount),
                                          SgstAmount = a.Sum(y => y.SgstAmount),
                                          CgstAmount = a.Sum(y => y.CgstAmount),
                                          IgstAmount = a.Sum(y => y.IgstAmount)
                                      })
                                      .FirstOrDefault();

            
            if (null != salePurchaseTotals) { 
                saleOrPurchaseReport.SalePurchaseDetailsTotal.Add( new SalePurchaseDetailModel
                                                                    {
                                                                        VouAmount = salePurchaseTotals.VouAmount,
                                                                        NetAmount = salePurchaseTotals.NetAmount,
                                                                        SgstAmount = salePurchaseTotals.SgstAmount,
                                                                        CgstAmount = salePurchaseTotals.CgstAmount,
                                                                        IgstAmount = salePurchaseTotals.IgstAmount
                                                                    });
             }

            //Account Headwise details
            var accountHeadSummary = registerDetail
                                      .GroupBy(a => new { a.AccountId, a.AccountName })
                                      .Select(a => new
                                      {
                                           
                                          AccountName = a.Key,
                                          VouAmount = a.Sum(y => y.VouAmount),
                                          NetAmount = a.Sum(y => y.NetAmount),
                                          SgstAmount = a.Sum(y => y.SgstAmount),
                                          CgstAmount = a.Sum(y => y.CgstAmount),
                                          IgstAmount = a.Sum(y => y.IgstAmount),
                                      })
                                      .ToList();

            foreach (var item in accountHeadSummary)
            {
                SalePurchaseDetailModel accountHead = new SalePurchaseDetailModel();

                accountHead.AccountId = item.AccountName.AccountId;
                accountHead.AccountName = item.AccountName.AccountName;
                accountHead.VouAmount = item.VouAmount;
                accountHead.NetAmount = item.NetAmount;
                accountHead.SgstAmount = item.SgstAmount;
                accountHead.CgstAmount = item.CgstAmount;
                accountHead.IgstAmount = item.IgstAmount;
                vouAmount += (decimal)item.VouAmount;
                netAmount += (decimal) item.NetAmount;
                sgstAmount += (decimal) item.SgstAmount;
                cgstAmount += (decimal) item.CgstAmount;
                igstAmount += (decimal)item.IgstAmount;
                saleOrPurchaseReport.AccountHeadSummary.Add(accountHead);
            }

            //Add AccountHeadSummaryTotal to report
            saleOrPurchaseReport.AccountHeadSummaryTotal.Add(new SalePurchaseDetailModel
            {
                VouAmount = vouAmount,
                NetAmount = netAmount,
                SgstAmount = sgstAmount,
                CgstAmount = cgstAmount,
                IgstAmount = igstAmount
            });

            netAmount = 0;
            sgstAmount = 0;
            cgstAmount = 0;
            igstAmount = 0;

            //CGST Headwise details
            var cgstHeadSummary = registerDetail
                                      .GroupBy(a => new { a.CgstId, a.CgstName })
                                      .Select(a => new
                                      {
                                          AccountName = a.Key,
                                          VouAmount = a.Sum(y => y.VouAmount),
                                          NetAmount = a.Sum(y => y.NetAmount),
                                          SgstAmount = a.Sum(y => y.SgstAmount),
                                          CgstAmount = a.Sum(y => y.CgstAmount),
                                          IgstAmount = a.Sum(y => y.IgstAmount)
                                      })
                                      .ToList();

            foreach (var item in cgstHeadSummary)
            {
                SalePurchaseDetailModel cgstHead = new SalePurchaseDetailModel();
                cgstHead.AccountId = item.AccountName.CgstId.ToString();
                cgstHead.AccountName = item.AccountName.CgstName;
                cgstHead.VouAmount = item.VouAmount;
                cgstHead.NetAmount = item.NetAmount;
                cgstHead.SgstAmount = item.SgstAmount;
                cgstHead.CgstAmount = item.CgstAmount;
                cgstHead.IgstAmount = item.IgstAmount;
                vouAmount += (decimal)item.VouAmount;
                netAmount += (decimal)item.NetAmount;
                sgstAmount += (decimal)item.SgstAmount;
                cgstAmount += (decimal)item.CgstAmount;
                igstAmount += (decimal)item.IgstAmount;
                saleOrPurchaseReport.CgstHeadSummary.Add(cgstHead);
            }

            //Add CgstHeadSummaryTotal to report
            saleOrPurchaseReport.CgstHeadSummaryTotal.Add(new SalePurchaseDetailModel
            {
                VouAmount = vouAmount,
                NetAmount = netAmount,
                SgstAmount = sgstAmount,
                CgstAmount = cgstAmount,
                IgstAmount = igstAmount
            });

            netAmount = 0;
            sgstAmount = 0;
            cgstAmount = 0;
            igstAmount = 0;

            //SGST Headwise details
            var sgstHeadSummary = registerDetail
                                      .GroupBy(a => new { a.SgstId , a.SgstName})
                                      .Select(a => new
                                      {
                                          AccountName = a.Key,
                                          VouAmount = a.Sum(y => y.VouAmount),
                                          NetAmount = a.Sum(y => y.NetAmount),
                                          SgstAmount = a.Sum(y => y.SgstAmount),
                                          CgstAmount = a.Sum(y => y.CgstAmount),
                                          IgstAmount = a.Sum(y => y.IgstAmount)
                                      })
                                      .ToList();

            foreach (var item in sgstHeadSummary)
            {
                SalePurchaseDetailModel sgstHead = new SalePurchaseDetailModel();
                sgstHead.AccountId = item.AccountName.SgstId.ToString();
                sgstHead.AccountName = item.AccountName.SgstName;
                sgstHead.VouAmount = item.VouAmount;
                sgstHead.NetAmount = item.NetAmount;
                sgstHead.SgstAmount = item.SgstAmount;
                sgstHead.CgstAmount = item.CgstAmount;
                sgstHead.IgstAmount = item.IgstAmount;
                vouAmount += (decimal)item.VouAmount;
                netAmount += (decimal)item.NetAmount;
                sgstAmount += (decimal)item.SgstAmount;
                cgstAmount += (decimal)item.CgstAmount;
                igstAmount += (decimal)item.IgstAmount;
                saleOrPurchaseReport.SgstHeadSummary.Add(sgstHead);
            }

            //Add SgstHeadSummaryTotal to report
            saleOrPurchaseReport.SgstHeadSummaryTotal.Add(new SalePurchaseDetailModel
            {
                VouAmount = vouAmount,
                NetAmount = netAmount,
                SgstAmount = sgstAmount,
                CgstAmount = cgstAmount,
                IgstAmount = igstAmount
            });

            netAmount = 0;
            sgstAmount = 0;
            cgstAmount = 0;
            igstAmount = 0;


            //IGST Headwise details
            var igstHeadSummary = registerDetail
                                      .GroupBy(a => new { a.IgstId, a.IgstName })
                                      .Select(a => new
                                      {
                                          AccountName = a.Key,
                                          VouAmount = a.Sum(y => y.VouAmount),
                                          NetAmount = a.Sum(y => y.NetAmount),
                                          SgstAmount = a.Sum(y => y.SgstAmount),
                                          CgstAmount = a.Sum(y => y.CgstAmount),
                                          IgstAmount = a.Sum(y => y.IgstAmount)
                                      })
                                      .ToList();


            foreach (var item in igstHeadSummary)
            {
                SalePurchaseDetailModel igstHead = new SalePurchaseDetailModel();
                igstHead.AccountId = item.AccountName.IgstId.ToString();
                igstHead.AccountName = item.AccountName.IgstName;
                igstHead.VouAmount = item.VouAmount;
                igstHead.NetAmount = item.NetAmount;
                igstHead.SgstAmount = item.SgstAmount;
                igstHead.CgstAmount = item.CgstAmount;
                igstHead.IgstAmount = item.IgstAmount;
                vouAmount += (decimal)item.VouAmount;
                netAmount += (decimal)item.NetAmount;
                sgstAmount += (decimal)item.SgstAmount;
                cgstAmount += (decimal)item.CgstAmount;
                igstAmount += (decimal)item.IgstAmount;
                saleOrPurchaseReport.IgstHeadSummary.Add(igstHead);
            }

            //Add IgstHeadSummaryTotal to report
            saleOrPurchaseReport.IgstHeadSummaryTotal.Add(new SalePurchaseDetailModel
            {
                VouAmount = vouAmount,
                NetAmount = netAmount,
                SgstAmount = sgstAmount,
                CgstAmount = cgstAmount,
                IgstAmount = igstAmount
            });

            serviceResponse.Data = saleOrPurchaseReport;
            return serviceResponse;

        }

        public async Task<ServiceResponseModel<LedgerReportModel>> GeneralLedger(ReportModel reportModel)
        {
            ServiceResponseModel<LedgerReportModel> serviceResponse = new ServiceResponseModel<LedgerReportModel>();

            LedgerReportModel lederReport = new LedgerReportModel();
            lederReport.LederReportDetailModel = new List<LederReportDetailModel>();
            List<LederReportDetailModel> lederLineHeadModels = new List<LederReportDetailModel>();


            //Select accounts order by accountId
            var AccountList = _context.AccountMasters
                                .Where(a => a.CompCode == reportModel.CompCode &&
                                       a.AccYear == reportModel.AccYear &&
                                       a.AccountId.CompareTo(reportModel.StartAccount) >= 0 &&
                                       a.AccountId.CompareTo(reportModel.FinishAccount) <= 0)
                                .OrderBy(a => a.CompCode).ThenBy(a=> a.AccYear).ThenBy(a=> a.AccountId)
                                .ToList();

            //Add company head to report
            lederReport.Company = _context.CompInfos.FirstOrDefault(a =>
                                    a.CompCode == reportModel.CompCode &&
                                    a.AccYear == reportModel.AccYear);

            foreach (var account in AccountList)
            {
                LederReportDetailModel lederLineHeadModel = new LederReportDetailModel();
                decimal dOpBal1 = 0;
                decimal dOpBal2 = 0;
                decimal dCurDr = 0;
                decimal dCurCr = 0;
                decimal dClBal = 0;

                //Calculate opening balance and update header
                lederLineHeadModel.AccountHeader = new AccountMasterModel { 
                                                            CompCode = account.CompCode,
                                                            AccYear = account.AccYear,
                                                            AccountId = account.AccountId,
                                                            Name = account.Name,
                                                            Opening = account.Opening};


                //Calculate transaction and calculate row balance
                lederLineHeadModel.AccountLedger = new List<LedgerDetalItemModel>();

                var ledgerList = (from ledger in _context.Ledgers
                         join acMst in _context.AccountMasters on ledger.CorrAccountId equals acMst.AccountId
                        where (ledger.AccountId == account.AccountId) && (ledger.VouDate <= reportModel.FinishDate )
                         select new LedgerDetalItemModel
                         {
                             CompCode  = ledger.CompCode,
                             AccYear = ledger.AccYear,
                             VouNo = ledger.VouNo,
                             VouDate = ledger.VouDate,
                             AccountId = ledger.AccountId,
                             TrxType =ledger.TrxType,
                             BilChq = ledger.BilChq,
                             DrCr = ledger.DrCr,
                             DrAmount = (ledger.DrCr=="1") ? ledger.Amount: null,
                             CrAmount  = (ledger.DrCr=="2") ? ledger.Amount: null,
                             ClAmount = 0,
                             CorrAccountId = ledger.CorrAccountId,
                             CorrAccountName = acMst.Name,
                             VouDetail = ledger.VouDetail
                         })
                         .OrderBy(a => a.CompCode).ThenBy(a=> a.AccYear).ThenBy(a=> a.AccountId).ThenBy(a=> a.VouDate)
                         .ToList();

                //account year opening balance
                dOpBal1 = account.Opening ?? 0;
                dOpBal2 = account.Opening ?? 0;
                foreach (var ledgerRow in ledgerList)
                {
                    if (ledgerRow.VouDate < reportModel.StartDate)
                    {
                        if (ledgerRow.DrCr == "1")
                        {
                            dOpBal1 = dOpBal1 - (decimal) ledgerRow.DrAmount;
                            dOpBal2 = dOpBal2 - (decimal) ledgerRow.DrAmount;
                        } 
                        else
                        {
                            dOpBal1 = dOpBal1 - (decimal) ledgerRow.CrAmount;
                            dOpBal2 = dOpBal2 - (decimal) ledgerRow.CrAmount;
                        }
                    }
                    else
                    {
                        if (ledgerRow.DrCr == "1")
                        {
                            dCurDr = dCurDr - (decimal)ledgerRow.DrAmount;
                        }
                        else
                        {
                            dCurCr = dCurCr - (decimal)ledgerRow.CrAmount;
                        }
                        ledgerRow.ClAmount = dOpBal1 + dCurCr - dCurDr;
                        dClBal = dOpBal1 + dCurCr - dCurDr;
                    }
                }

                lederLineHeadModel.AccountLedger.AddRange(ledgerList);

                //Update opening balance for account
                lederLineHeadModel.AccountHeader.Opening = dOpBal1;

                //Calculate closing balance and update footer
                if (dOpBal2 > 0)
                {
                    dCurDr = dCurDr - dOpBal2;
                }
                else
                {
                    dCurCr = dCurCr + dOpBal2;
                }
                dClBal = dCurCr - dCurDr ;
                lederLineHeadModel.AccountFooter = new AccountMasterModel
                                                            {
                                                                AccountId = account.AccountId,
                                                                Name = account.Name,
                                                                CurCr = dCurCr,
                                                                CurDr = dCurDr,
                                                                Closing = dClBal
                                                            };

                lederLineHeadModels.Add(lederLineHeadModel);
            }

            lederReport.LederReportDetailModel.AddRange(lederLineHeadModels);
            serviceResponse.Data = lederReport;
            return serviceResponse;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
